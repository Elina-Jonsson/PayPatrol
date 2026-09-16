namespace PayPatrol.Api.Middleweres
{
    // This middleware is responsible for extracting the JWT token from the cookies and adding it to the Authorization header of the HTTP request.
    public class JwtCookieMiddlewere
    {
        private readonly RequestDelegate _next;
        public JwtCookieMiddlewere(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Cookies.TryGetValue("jwt", out var token)  // Check if the "jwt" cookie exists and retrieve its value
                && !context.Request.Headers.ContainsKey("Authorization")) // Check if the "Authorization" header is not already present in the request
            {
                context.Request.Headers.Append("Authorization", $"Bearer {token}");  // Add the JWT token to the "Authorization" header in the format "Bearer {token}"
            }

            await _next(context); // Call the next middleware in the pipeline to continue processing the request
        }
    }
}
