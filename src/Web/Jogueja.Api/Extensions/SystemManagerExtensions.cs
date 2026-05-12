namespace Jogueja.Api.Extensions
{
    public static class SystemsManagerExtensions
    {
        public static void ConfigureSystemsManager(this WebApplicationBuilder builder)
        {
            builder.Configuration
                .AddSystemsManager($"/common")
                .AddSystemsManager($"/jogueja-api");
        }
    }
}
