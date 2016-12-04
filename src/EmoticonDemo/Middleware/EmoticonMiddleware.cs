using System.IO;
using System.Threading.Tasks;
using EmoticonDemo.ActionFilter;
using Microsoft.AspNetCore.Http;

namespace EmoticonDemo.Middleware
{
    public class EmoticonMiddleware
    {
        private readonly RequestDelegate _next;

        public EmoticonMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using (var buffer = new MemoryStream())
            {
                //replace the context response with our buffer
                var stream = context.Response.Body;
                context.Response.Body = buffer;

                // Invoke the rest of the pipeline 
                // if there are any other middleware components
                await _next.Invoke(context);

                // Reset and read out the contents
                buffer.Seek(0, SeekOrigin.Begin);

                // Adjust the response stream to include our images.
                var emoticonStream = new EmoticonStream(stream);

                // Reset the stream again
                buffer.Seek(0, SeekOrigin.Begin);

                // Copy our content to the original stream and put it back
                await buffer.CopyToAsync(emoticonStream);
                context.Response.Body = emoticonStream;
            }

        }
    }
}