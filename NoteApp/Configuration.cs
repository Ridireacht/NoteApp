using IdentityServer4.Models;


namespace NoteApp
{
    public class Configuration
    {
        // ApiScope описывает то, что приложению можно использовать (доступ к областям)
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope();
            };


        // Области на сервере представлены ресурсами - Identity и API.
        // Identity-ресурс моделирует область, позволяющая приложению
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource> { }
    }
}
