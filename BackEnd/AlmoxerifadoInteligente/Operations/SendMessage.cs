using System;
using System.IO;
using System.Net;
using System.Text;
using static Program;

namespace RaspagemMagMer.Operations
{
    public class SendMessage
    {
        public static async void EnviarMsg(int produtoid,string phoneNumber,string nomeProduto, string nomeMag, string precoMag, string nomeMerc, string precoMerc, string responseBench)
        {

                try
                {

                    var parameters = new System.Collections.Specialized.NameValueCollection();
                    var client = new System.Net.WebClient();

                    var url = "https://app.whatsgw.com.br/api/WhatsGw/Send/";

                    parameters.Add("apikey", "dd42786b-64c9-49fd-9b28-4a82a005a98d");
                    parameters.Add("phone_number", "5579998626376"); 
                    parameters.Add("contact_phone_number", phoneNumber); 
                    parameters.Add("message_custom_id", "teste");
                    parameters.Add("message_type", "text");
                    parameters.Add("message_body",
                               "*Resultado da Comparação de Preços*" +
                               "\n" +
                               $"*Produto Pesquisado*: {nomeProduto}\n" +
                               "\n" +
                               $"*Mercado Livre*:\n" +
                               $"*Nome*: {nomeMerc} \n" +
                               $"*Preço*: R$ {precoMerc}\n" +
                               "\n" +
                               $"*Magazine Luiza*:\n" +
                               $"*Nome*: {nomeMag} \n" +
                               $"*Preço*: {precoMag}\n" +
                               "\n" +
                               $"*Resultado*:\n" +
                               $"{responseBench}\n" +
                               "\n" +
                               "Robo: 1806\n" +
                               "Usuario: rafaelMecenas"
                       );

                    byte[] response_data;
                    string responseString = "";

                    response_data = await client.UploadValuesTaskAsync(url, "POST", parameters);
                    responseString = Encoding.UTF8.GetString(response_data);
                    Console.WriteLine("Response String: " + responseString);
                    LogRegister.RegistrarLog(DateTime.Now, "SendZap", "Sucesso", produtoid);


            }
                catch (Exception ex)
                {
                LogRegister.RegistrarLog(DateTime.Now, "SendZap", "Erro", produtoid);
                Console.WriteLine($"Erro na Mensagem: {ex.Message}");
                Console.WriteLine("Vale a pena conferir se a extensão do Whatssap está Incluida no navegador!");
                }
            
            

        }

        public static string OpcaoMsg() {
            Console.WriteLine("Você Deseja Receber os Dados pelo telefone? Sim ou Nao");
            string opt = Console.ReadLine();
            string num = "";
            if (opt.ToUpper().Equals("SIM"))
            {
                Console.WriteLine("Insira o Número do Whatssap: DDD + Número (Apenas Números)");
                num = "55" + Console.ReadLine();
                return num;
            }
            return null;
        }
    }
}