using App.Application.Interfaces;
using App.Domain.Interfaces;
using App.Infra.Data.Context;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace App.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration config)
        {
            ///     variables
            ///     


            ////=======================================================================
            ///
            ///  INSTACIAS DE SERVICES
            /// 
            ///

            services.AddScoped<IPedidosService, PedidosService>();
            ////=======================================================================
            ///
            ///  INSTACIAS DE REPOSITORY
            /// 
            ///
            services.AddScoped<IPedidosRepository, PedidoRepository>();

            ////=======================================================================
            ///
            ///  INSTACIAS DE CONTEXTO
            /// 
            ///
            services.AddDbContext<MySQLContext>(options =>
              options.UseMySql(
                  config["ConnectionPedidos"],
                  ServerVersion.AutoDetect(config["ConnectionPedidos"])
              ));
            services.AddScoped<MySQLContext>();




        }
    }
}
