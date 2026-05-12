using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Configuration
{
    public interface IModuleInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration);
    }
}
