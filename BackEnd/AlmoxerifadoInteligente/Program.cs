using AlmoxerifadoInteligente.API;
using AlmoxerifadoInteligente.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RaspagemMagMer.Operations;

class Program
{
    public static void Main(string[] args)
    {
        // Iniciar o host da API antes de solicitar informa��es do usu�rio
        var host = CreateHostBuilder(args).Build();
        host.RunAsync();

        DBCheck.VerificarNovoProduto();

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

