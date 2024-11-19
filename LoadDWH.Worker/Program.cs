using LoadDWH.Data.Context;
using LoadDWH.Data.Interfaces;
using LoadDWH.Data.Services;
using LoadDWH.Worker;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            
            services.AddDbContextPool<NorwindContext>(options =>
                options.UseSqlServer(hostContext.Configuration.GetConnectionString("Norwind")));

            
            services.AddDbContextPool<SalesContext>(options =>
                options.UseSqlServer(hostContext.Configuration.GetConnectionString("Sales")));

            
            services.AddScoped<IDataServiceDwVenta, DataServiceDwVentas>();

            
            services.AddHostedService<Worker>();
        });
}
