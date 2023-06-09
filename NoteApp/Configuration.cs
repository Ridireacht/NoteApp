﻿using IdentityModel;
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
                new ApiScope("NoteAppWebAPI", "Web API")
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
                new ApiResource("NoteAppWebAPI", "Web API", new [] { JwtClaimTypes.Name})
                {
                    Scopes = { "NoteAppWebAPI" }
                }
            };


        // Clients - список приложений с определёнными правами, которые могут использовать систему
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "note-app-web-app",
                    ClientName = "NoteApp Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
						"http://localhost:25798/signin-oidc"
					},
                    AllowedCorsOrigins =
                    {
						"http://localhost:25798"
					},
                    PostLogoutRedirectUris =
                    {
						"http://localhost:25798/signout-oidc"
					},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
						"NoteAppWebAPI"
					},
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}
