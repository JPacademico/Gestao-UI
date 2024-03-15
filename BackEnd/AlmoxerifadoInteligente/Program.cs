using AlmoxerifadoInteligente.API;
using AlmoxerifadoInteligente.Models;
using AlmoxerifadoInteligente.Operations.Register;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RaspagemMagMer.Operations;

class Program
{
    public static void Main(string[] args)
    {
        // Iniciar o host da API antes de solicitar informações do usuário
        var host = CreateHostBuilder(args).Build();
        host.RunAsync().GetAwaiter().GetResult(); // Espera a conclusão da execução do host

        // Agora você pode verificar o novo produto
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dbCheck = services.GetRequiredService<DBCheck>();
            dbCheck.VerificarNovoProduto();
        }

        // Agora você pode esperar o host da API terminar de executar
        host.WaitForShutdown();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<ApiStarter>(); 
            });
}

