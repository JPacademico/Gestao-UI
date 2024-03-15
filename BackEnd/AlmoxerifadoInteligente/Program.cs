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
        // Iniciar o host da API antes de solicitar informa��es do usu�rio
        var host = CreateHostBuilder(args).Build();
        host.RunAsync().GetAwaiter().GetResult(); // Espera a conclus�o da execu��o do host

        // Agora voc� pode verificar o novo produto
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var dbCheck = services.GetRequiredService<DBCheck>();
            dbCheck.VerificarNovoProduto();
        }

        // Agora voc� pode esperar o host da API terminar de executar
        host.WaitForShutdown();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<ApiStarter>(); 
            });
}

