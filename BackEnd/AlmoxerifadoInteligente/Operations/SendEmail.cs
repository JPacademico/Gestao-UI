
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
        public static bool EnviarEmail(string destinatario)
        {

            string smtpserver = "smtp-mail.outlook.com";
            int porta = 587;
            string remetente = "rafaelMecenasRobo@outlook.com";
            string senha = "teste123@";


            using (SmtpClient client = new SmtpClient(smtpserver, porta))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(remetente, senha);
                client.EnableSsl = true;

                MailMessage mensagem = new(remetente, destinatario)
                {

                   

                };

                try
                {
                    client.Send(mensagem);
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro no Email: " + ex.Message);
                    return false;
                }

            }

        }

        public static string OpcaoEmail()
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Console.Write("Insira o Email para recebimento do Resultado: ");
            string email = Console.ReadLine();
            bool opt = (Regex.IsMatch(email, pattern));
            if (opt)
            {
                Console.WriteLine("Email Informado: " + email);
                return email;
            }
            while (opt == false)
            {
                Console.WriteLine("Email Inválido, tente novamente!");
                OpcaoEmail();

            }
            return null;
        }
    }

}



