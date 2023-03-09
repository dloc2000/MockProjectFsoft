namespace Fsoft.Web.Locdx7.API.Middleware
{
    public class CheckAcessMiddleware
    {
        // Biến tham chiếu đến middelware tiếp theo trong pipeline
        // Thực chất là đang gọi Middleware kế tiếp
        private readonly RequestDelegate _next;

        public CheckAcessMiddleware (RequestDelegate next)
        {
            _next = next;
        }

        // Phương thức được gọi khi Request tới Middleware
        public async Task Invoke(HttpContext httpContext)
        {
            if(httpContext.Request.Path == "/api/v1/admin")
            {
                Console.WriteLine("Cấm truy cập");
                await Task.Run(
                    async () =>
                    {
                        string html = "<h1>Ban phai la Admin de truy cap tinh nang nay<h1>";
                        httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                        await httpContext.Response.WriteAsync(html);
                    }
                    );
            } else
            {


                // Thiết lập header cho HttpResponse
                httpContext.Response.Headers.Add("throughCheckAcessMiddleware", new[] { DateTime.Now.ToString() });

                Console.WriteLine("CheckAcessMiddleware: Cho truy cập");

                // StaticFileMiddleware -> SessionMiddleware -> EndpointMiddleware -> endp1 , endp2
                // Chuyển Middleware tiếp theo trong pipeline
                await _next(httpContext);
            }
        }
    }
}
