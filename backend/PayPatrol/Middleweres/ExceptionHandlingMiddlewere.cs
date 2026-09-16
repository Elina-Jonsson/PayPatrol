namespace PayPatrol.Api.Middleweres
{
    // This middleware is responsible for handling exceptions that occur during the processing of HTTP requests.
    // It logs the exception and returns a standardized error response to the client.
    public class ExceptionHandlingMiddlewere
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddlewere> _logger;

        public ExceptionHandlingMiddlewere(RequestDelegate next, ILogger<ExceptionHandlingMiddlewere> logger)
        {
            _next = next; // Send the request to the next middleware in the pipeline.
            _logger = logger; // Tool to log exceptions and other information for debugging and monitoring purposes.
        }

        // This method is called for each HTTP request and is responsible for handling exceptions that occur during request processing.
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");  // Log the exception details for debugging and monitoring purposes.
                context.Response.StatusCode = 500;                        // Set the HTTP status code to 500 (Internal Server Error) to indicate that an unexpected error occurred.
                context.Response.ContentType = "application/json";        // The response data is sent in JSON format
                var errorResponse = new { message = "An unexpected error occurred. Please try again later." };  // Create an anonymous object containing a user-friendly error message to be sent in the response body.
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
