using System.Globalization;

namespace AlmoxerifadoInteligente.Utils
{
    public abstract class ConvertToBRL
    {
        public static decimal StringToDecimal(string price)
        {
            CultureInfo cultureInfo = new CultureInfo("pt-BR");

            string cleanedPrice = price.Replace("R$","").Trim();

            decimal result = decimal.Parse(cleanedPrice, NumberStyles.Currency ,cultureInfo);

            return result;
        }
    }
}
