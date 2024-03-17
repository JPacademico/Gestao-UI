
using AlmoxerifadoInteligente.Models;
using AlmoxerifadoInteligente.Operations.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Program;

namespace RaspagemMagMer.Operations
{
    public class SendEmail
    {
        private readonly AlmoxarifadoDBContext _dbContext;
        private readonly LogRegister _logRegister;

        public SendEmail(AlmoxarifadoDBContext dbContext, LogRegister logRegister)
        {
            _dbContext = dbContext;
            _logRegister = logRegister;
        }

        public bool EnviarEmail(string destinatario, int idProduto)
        {
            var produto = _dbContext.BenchmarkingItem.FirstOrDefault(p => p.IdProduto == idProduto);

            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado.");
                return false;
            }

            string smtpserver = "smtp-mail.outlook.com";
            int porta = 587;
            string remetente = "rafaelMecenasRobo@outlook.com";
            string senha = "teste123@";


            using (SmtpClient client = new SmtpClient(smtpserver, porta))
            {
                string responseBench = string.Empty;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(remetente, senha);
                client.EnableSsl = true;

                if (produto.PrecoLoja2 > produto.PrecoLoja1)
                {
                    responseBench = $"O preço do produto está melhor no Mercado livre, pois está R$ {produto.Economia} mais barato\n" +
                       $"Link para produto Mer: {produto.LinkLoja1}";
                    Console.WriteLine(responseBench);
                }
                else
                {
                    responseBench = $"O preço do produto está melhor na Magazine Luiza, pois está R$ {produto.Economia} mais barato\n" +
                       $"Link para produto Mag: {produto.LinkLoja2}";
                    Console.WriteLine(responseBench);
                }
                MailMessage mensagem = new(remetente, destinatario)
                {

                    Subject = "Resultado da Comparação de Preços",
                    Body = "\n" +
                           $"Mercado Livre:\n" +
                           $"Nome: {produto.NomeLoja1} \n" +
                           $"Preço: R$ {produto.PrecoLoja1}\n" +
                           "\n" +
                           $"Magazine Luiza:\n" +
                           $"Nome: {produto.NomeLoja2} \n" +
                           $"Preço: R$ {produto.PrecoLoja2}\n" +
                           "\n" +
                           $"Resultado:\n"
                           +
                           $"{responseBench}\n" +
                           "\n" +
                           "Robo: 1806\n" +
                           "Usuario: rafaelMecenas"

                };

                try
                {
                    client.Send(mensagem);
                    _logRegister.RegistrarLog(DateTime.Now, "SendEmail", "Sucesso", idProduto);
                    return true;

                }
                catch (Exception ex)
                {
                    _logRegister.RegistrarLog(DateTime.Now, "SendEmail", "Erro: "+ex.Message, idProduto);
                    Console.WriteLine("Erro no Email: " + ex.Message);
                    return false;
                }

            }

        }
    }

}



