using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ClaudeFunction.Domain.Services;

[assembly: FunctionsStartup(typeof(ClaudeFunction.Startup))]

namespace ClaudeFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IMessageTransformationService, MessageTransformationService>();
        }
    }
}
