using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;


namespace NoteApp
{

    // Настройка IdentityServer'а
    public class Configuration
    {

        // ApiScope описывает то, что приложению можно использовать (доступ к областям)
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("sample-scope")
            };


        // Области на сервере представлены ресурсами - Identity и API.
        // Identity-ресурс моделирует область, позволяющая приложению видеть утверждения (claim'ы) о пользователе
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };


        // API-ресурс моделирует доступ ко всему ресурсу
        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("sample-api-resource", new [] { JwtClaimTypes.Name})
                {
                    Scopes = {"sample-scope"}
                }
            };


        // Clients - список приложений с определёнными правами, которые могут использовать систему
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "sample-id",
                    ClientName = "sample-name",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        "http://.../signin-oidc"
                    },
                    AllowedCorsOrigins =
                    {
                        "http://..."
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://.../signout-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "sample-scope"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}
