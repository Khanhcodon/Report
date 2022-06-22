using System.Security.Cryptography;

namespace Bkav.eGovCloud.Oauth
{
    public static class EncryptionKeys
    {
        private static readonly RSACryptoServiceProvider provider = new RSACryptoServiceProvider();

        private static readonly RSAParameters ResourceServerEncryptionPublicKey = provider.ExportParameters(true);

        private static readonly RSAParameters ResourceServerEncryptionPrivateKey = provider.ExportParameters(true);

        private static readonly RSAParameters AuthorizationServerSigningPrivateKey = provider.ExportParameters(true);

        private static readonly RSAParameters AuthorizationServerSigningPublicKey = provider.ExportParameters(true);

        public static RSACryptoServiceProvider GetResourceServerEncryptionPublicKey()
        {
            return CreateRsaCryptoServiceProvider(ResourceServerEncryptionPublicKey);
        }

        public static RSACryptoServiceProvider GetResourceServerEncryptionPrivateKey()
        {
            return CreateRsaCryptoServiceProvider(ResourceServerEncryptionPrivateKey);
        }

        public static RSACryptoServiceProvider GetAuthorizationServerSigningPrivateKey()
        {
            return CreateRsaCryptoServiceProvider(AuthorizationServerSigningPrivateKey);
        }

        public static RSACryptoServiceProvider GetAuthorizationServerSigningPublicKey()
        {
            return CreateRsaCryptoServiceProvider(AuthorizationServerSigningPublicKey);
        }

        private static RSACryptoServiceProvider CreateRsaCryptoServiceProvider(RSAParameters parameters)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(parameters);
            return rsa;
        }
    }
}