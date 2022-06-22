using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Bkav.eGovCloud.Entities.Customer;
using Bkav.eGovOnline.Business.Customer;
using DotNetOpenAuth.Messaging.Bindings;

namespace Bkav.eGovCloud.Oauth
{
    internal static class Utilities
    {
        internal static DateTime AsUtc(this DateTime value)
        {
            if (value.Kind == DateTimeKind.Unspecified)
            {
                return new DateTime(value.Ticks, DateTimeKind.Utc);
            }

            return value.ToUniversalTime();
        }
    }

    public class DatabaseKeyNonceStore : INonceStore, ICryptoKeyStore
    {
        private readonly ClientBll _clientService;

        public DatabaseKeyNonceStore()
        {
            _clientService = DependencyResolver.Current.GetService<ClientBll>();
        }

        /// <summary>
        /// Stores a given nonce and timestamp.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="nonce"></param>
        /// <param name="timestampUtc"></param>
        /// <returns>true nếu context + nonce +timestamputc là duy nhất</returns>
        public bool StoreNonce(string context, string nonce, DateTime timestampUtc)
        {
            return _clientService.CreateNonce(new Nonce { Context = context, Code = nonce, TimeStamp = timestampUtc });
        }

        public CryptoKey GetKey(string bucket, string handle)
        {
            var key = _clientService.GetKey(bucket, handle);
            var matches = new CryptoKey(key.Secret, key.ExpiresUtc.AsUtc());
            return matches;
        }

        public IEnumerable<KeyValuePair<string, CryptoKey>> GetKeys(string bucket)
        {
            var keys = _clientService.GetKeys(bucket);
            var result = new List<KeyValuePair<string, CryptoKey>>();
            foreach (var key in keys)
            {
                result.Add(new KeyValuePair<string, CryptoKey>(key.Handle, new CryptoKey(key.Secret, key.ExpiresUtc.AsUtc())));
            }
            return result;
        }

        public void StoreKey(string bucket, string handle, CryptoKey key)
        {
            var keyRow = new SymmetricCryptoKey()
            {
                Bucket = bucket,
                Handle = handle,
                Secret = key.Key,
                ExpiresUtc = key.ExpiresUtc,
            };
            _clientService.CreateKey(keyRow);
        }

        public void RemoveKey(string bucket, string handle)
        {
            var key = _clientService.GetKey(bucket, handle);
            _clientService.RemoveKey(key);
        }
    }
}