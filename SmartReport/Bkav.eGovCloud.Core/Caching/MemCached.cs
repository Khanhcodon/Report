using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;

namespace Bkav.eGovCloud.Core.Caching
{
    /// <summary>
    /// Quản lý truy cập Memcached
    /// </summary>
    public class MemCached : ICache
    {
        private static string PREFIX = "BkaveGov";
        private readonly static object s_lock = new object();
        private string _domainName;
        //private MemcachedClient _cache;

#pragma warning disable 1591

        /// <summary>
        /// Khởi tạo
        /// </summary>
        public MemCached()
        {
            _domainName = PREFIX;
            //CreateLogFactory();
            //if (_cache == null)
            //{
            //    _cache = GetClient();
            //}
        }

        /// <summary>
        /// Khởi tạo
        /// </summary>
        /// <param name="domainName"></param>
        public MemCached(string domainName)
        {
            _domainName = domainName;
            if (string.IsNullOrWhiteSpace(_domainName))
            {
                _domainName = PREFIX;
            }

            //if (_cache == null)
            //{
            //    _cache = GetClient();
            //}
        }

        public T Get<T>(string key)
        {
            key = BuildKey(key);
            using (var _cache = new MemcachedClient())
            {
                try
                {
                    var result = _cache.Get(key);
                    return ByteArrayToObject<T>((byte[])result);
                }
                catch
                {
                    var result = _cache.Get<T>(key);
                    return result;
                }
            }
        }

        public T Get<T>(string key, Func<T> acquirer, int? _cacheTime)
        {
            key = BuildKey(key);

            using (var _cache = new MemcachedClient())
            {
                var result = Get<T>(key);
                if (result != null)
                {
                    return result;
                }

                lock (s_lock)
                {
                    var value = acquirer();
                    Set(key, value, _cacheTime);
                    return value;
                }
            }
        }

        public void Set(string key, object value, int? _cacheTime)
        {
            if (value == null)
            {
                return;
            }

            key = BuildKey(key);
            var policy = _cacheTime.GetValueOrDefault() > 0 ? new TimeSpan(0, _cacheTime.Value, 0) : new TimeSpan(0, 1, 0);
            // var cacheValue = ObjectToByteArray(value);

            using (var _cache = new MemcachedClient())
            {
                var isFinished = _cache.Store(StoreMode.Set, key, value, policy);
                if (isFinished)
                {
                    
                }
            }
        }

        public bool Contains(string key)
        {
            key = BuildKey(key);
            using (var _cache = new MemcachedClient())
            {
                var isAdded = _cache.Store(StoreMode.Add, key, "", new DateTime(2030, 1, 1));
                if (isAdded)
                {
                    _cache.Remove(key);
                }

                return !isAdded;
            }
        }

        public void Remove(string key)
        {
            key = BuildKey(key);
            using (var _cache = new MemcachedClient())
            {
                _cache.Remove(key);
            }
        }

        public void Clear()
        {
            using (var _cache = new MemcachedClient())
            {
                _cache.FlushAll();
            }
        }

        private string BuildKey(string key)
        {
            return !string.IsNullOrEmpty(key) ? key.Replace("$domain", _domainName) : null;
        }

        private MemcachedClient GetClient()
        {
            CreateLogFactory();

            var config = new MemcachedClientConfiguration();
            config.AddServer("127.0.0.1", 11211);
            config.Protocol = MemcachedProtocol.Binary;

            return new MemcachedClient(config);
        }

        private void CreateLogFactory()
        {
            var logPath = FileSystem.FileUtil.GetRandomTimeFile("C:\\Log", "log", "", "", "DDMMYYYhhmm");
            LogManager.AssignFactory(new DiagnosticsLogFactory(logPath));
        }

        private static byte[] ObjectToByteArray<T>(T obj)
        {
            if (obj == null)
            {
                return null;
            }

            var bf = new BinaryFormatter();
            var mf = new MemoryStream();
            bf.Serialize(mf, obj);
            return mf.ToArray();
        }

        private static T ByteArrayToObject<T>(byte[] bytes)
        {
            var mf = new MemoryStream();
            var bf = new BinaryFormatter();
            mf.Write(bytes, 0, bytes.Length);
            mf.Seek(0, SeekOrigin.Begin);
            return (T)bf.Deserialize(mf);
        }

        #region NotImplemented

        public IEnumerable<KeyValuePair<string, object>> Entries
        {
            get { throw new NotImplementedException(); }
        }

        #endregion


        public Dictionary<string, string> GetSizeOfMemories()
        {
            throw new NotImplementedException();
        }
    }
}
