using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Bkav.eGovCloud.Core.Utils;

namespace Bkav.eGovCloud.Core.Caching
{
    /// <summary>
    /// Quản lý truy cập ObjectCache
    /// </summary>
    public class StaticCache : ICache
    {
        private const string PREFIX = "BkaveGov";
        private readonly static object s_lock = new object();
        private string _domainName;

#pragma warning disable 1591

        public StaticCache()
        {
            _domainName = PREFIX;
        }

        public StaticCache(string domainName)
        {
            _domainName = domainName;
            if (string.IsNullOrWhiteSpace(_domainName))
            {
                _domainName = PREFIX;
            }
        }

        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        public IEnumerable<KeyValuePair<string, object>> Entries
        {
            get
            {
                return from entry in Cache
                       where entry.Key.StartsWith(_domainName)
                       select new KeyValuePair<string, object>(
                           entry.Key.Substring(_domainName.Length),
                           entry.Value);
            }
        }

        public T Get<T>(string key)
        {
            key = BuildKey(key);
            return (T)Cache.Get(key);
        }

        public T Get<T>(string key, Func<T> acquirer, int? cacheTime)
        {
            key = BuildKey(key);

            if (Cache.Contains(key))
            {
                return (T)Cache.Get(key);
            }
            else
            {
                lock (s_lock)
                {
                    if (!Cache.Contains(key))
                    {
                        var value = acquirer();
                        if (value != null)
                        {
                            Set(key, value, cacheTime);
                        }

                        return value;
                    }
                }

                return (T)Cache.Get(key);
            }
        }

        public void Set(string key, object data, int? cacheTime)
        {
            key = BuildKey(key);
            var cacheItem = new CacheItem(key, data);
            CacheItemPolicy policy = null;
            if (cacheTime.GetValueOrDefault() > 0)
            {
                policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime.Value) };
            }

            Cache.Add(cacheItem, policy);
        }

        public bool Contains(string key)
        {
            return Cache.Contains(BuildKey(key));
        }

        public void Remove(string key)
        {
            Cache.Remove(BuildKey(key));
        }

        public void Clear()
        {
            foreach (var item in Cache)
            {
                var key = BuildKey(item.Key);
                Remove(key);
            }
        }

        private string BuildKey(string key)
        {
            return !string.IsNullOrEmpty(key) ? key.Replace("$domain", _domainName) : null;
        }

        public Dictionary<string, string> GetSizeOfMemories()
        {
            var result = new Dictionary<string, string>();
            decimal total = 0;
            foreach (var item in Entries)
            {
                using (Stream s = new MemoryStream())
                {
                    // BinaryFormatter formatter = new BinaryFormatter();
                    var size = item.Value.Stringify().Length;
                    total += size;
                    result.Add(item.Key, Core.Utils.StringExtension.ReadFileSize(size));
                }
            }

            result.Add("Tổng", Core.Utils.StringExtension.ReadFileSize(total));

            return result;
        }
    }
}
