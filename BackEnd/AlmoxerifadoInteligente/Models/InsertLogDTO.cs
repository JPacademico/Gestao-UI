using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;


public class InsertLogDTO
{
    public int codigoRobo { get; set; }
    public string? nomedev { get; set; }
    public string? nomeproduto { get; set; }
    public double? valor1 { get; set; }
    public double? valor2 { get; set; }
    public double? economia { get; set; }

    public InsertLogDTO(int codigoRobo, string nomedev, string nomeproduto, double valor1, double valor2, double economia)
    {
        this.codigoRobo = codigoRobo;
        this.nomedev = nomedev;
        this.nomeproduto = nomeproduto;
        this.valor1 = valor1;
        this.valor2 = valor2;
        this.economia = economia;
    }

    public async Task InsertAsync()
    {
        using (var client = new HttpClient())
        {
            string endpoint = "http://gestaomargi-001-site8.gtempurl.com/api/Logs";
            var json = JsonSerializer.Serialize(this);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(endpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Falha ao enviar os dados. Código de status: {response.StatusCode}. Mensagem: {errorMessage}");
            }
        }
    }
}
