using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bkav.eGovCloud.Areas.Admin.Models;
using Bkav.eGovOnline.Business.Customer;
using DotNetOpenAuth.Messaging.Bindings;
using DotNetOpenAuth.OAuth2;
using DotNetOpenAuth.OAuth2.ChannelElements;
using DotNetOpenAuth.OAuth2.Messages;

namespace Bkav.eGovCloud.Oauth
{
    public class AuthorizationServerHost : IAuthorizationServerHost
    {
        private readonly ClientBll _clientService;
        private readonly ScopeAreaBll _scopeAreaService;

        public AuthorizationServerHost()
        {
            _clientService = DependencyResolver.Current.GetService<ClientBll>();
            _scopeAreaService = DependencyResolver.Current.GetService<ScopeAreaBll>();
        }

        public AutomatedAuthorizationCheckResponse CheckAuthorizeClientCredentialsGrant(IAccessTokenRequest accessRequest)
        {
            throw new NotImplementedException();
        }

        public AutomatedUserAuthorizationCheckResponse CheckAuthorizeResourceOwnerCredentialGrant(string userName, string password, IAccessTokenRequest accessRequest)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Tạo 1 accesstoken sử dụng được trong 1 phút
        /// </summary>
        /// <param name="accessTokenRequestMessage"></param>
        /// <returns>accesstoken</returns>
        public AccessTokenResult CreateAccessToken(IAccessTokenRequest accessTokenRequestMessage)
        {
            var accessToken = new AuthorizationServerAccessToken();
            //Để 600 phút để test
            accessToken.Lifetime = TimeSpan.FromMinutes(600);
            accessToken.ResourceServerEncryptionKey = EncryptionKeys.GetResourceServerEncryptionPublicKey();
            accessToken.AccessTokenSigningKey = EncryptionKeys.GetAuthorizationServerSigningPrivateKey();
            return new AccessTokenResult(accessToken);
        }

        /// <summary>
        /// Lấy ra client
        /// </summary>
        /// <param name="clientIdentifier">clientIdentifier</param>
        /// <returns>model client</returns>
        public IClientDescription GetClient(string clientIdentifier)
        {
            var client = _clientService.GetByIdentifier(clientIdentifier);
            if (client == null)
            {
                throw new ArgumentOutOfRangeException("clientIdentifier");
            }
            return client.ToModel();
        }

        /// <summary>
        /// Kiểm tra client request đến phần có nằm scope được phân quyền không
        /// </summary>
        /// <param name="authorization">clientIdentifier</param>
        /// <returns>Nếu nằm trong vùng được phân quyền =>true, nếu không => false</returns>
        public bool IsAuthorizationValid(IAuthorizationDescription authorization)
        {
            var client = _clientService.GetByIdentifier(authorization.ClientIdentifier);
            var scopeAreas = _scopeAreaService.GetScopeAreas(client.Id);
            return this.IsAuthorizationValid(authorization.Scope, authorization.ClientIdentifier, authorization.UtcIssued, authorization.User);
        }

        private bool IsAuthorizationValid(HashSet<string> requestedScopes, string clientIdentifier, DateTime issuedUtc, string username)
        {
            // If db precision exceeds token time precision (which is common), the following query would
            // often disregard a token that is minted immediately after the authorization record is stored in the db.
            // To compensate for this, we'll increase the timestamp on the token's issue date by 1 second.
            issuedUtc += TimeSpan.FromSeconds(1);
            var client = _clientService.GetByIdentifier(clientIdentifier);
            var scopeAreas = _scopeAreaService.GetScopeAreas(client.Id);

            if (!scopeAreas.Any())
            {
                return false;
            }

            var grantedScopes = new HashSet<string>(OAuthUtilities.ScopeStringComparer);
            foreach (var scope in scopeAreas)
            {
                grantedScopes.UnionWith(OAuthUtilities.SplitScopes(scope.Scopes));
            }

            return requestedScopes.IsSubsetOf(grantedScopes);
        }

        /// <summary>
        /// Gets the store for storing crypto keys used to symmetrically encrypt and sign authorization codes and refresh tokens.
        /// </summary>
        public ICryptoKeyStore CryptoKeyStore
        {
            get { return ClientModel.KeyNonceStore; }
        }

        /// <summary>
        /// Authorization code nonce store để chắc chắn authorization codes chỉ được sử dụng 1 lần
        /// </summary>
        public INonceStore NonceStore
        {
            get { return ClientModel.KeyNonceStore; }
        }
    }
}