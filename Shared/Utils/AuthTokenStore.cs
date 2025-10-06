namespace Shared.Utils
{
    public static class AuthTokenStore
    {
        // Store centralizado del token de autenticación para toda la app
        public static string? Token { get; set; }
    }
}
