
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace AITravelAgent.Plugins.ConvertCurrency;

public class CurrencyConverter
{
    [KernelFunction, Description("Convert an amount from one currency to another")]
    public static string ConvertAmount(
        [Description("The target currency code")] string targetCurrencyCode,
        [Description("The amount to convert")] string amount,
        [Description("The starting currency code")] string baseCurrencyCode
        )
    {
        var currencyDictionary = Currency.Currencies;

        Currency targetCurrency = currencyDictionary[targetCurrencyCode];
        Currency baseCurrency = currencyDictionary[baseCurrencyCode];

        if (targetCurrency is null)
            return targetCurrency + " was not found";
        if (baseCurrency is null)
            return baseCurrency + " was not found";

        double amountInUSD = Double.Parse(amount) * baseCurrency.USDPerUnit;
        double result = amountInUSD * targetCurrency.UnitsPerUSD;

        return $@"{amount} {baseCurrencyCode} is approximately {result.ToString("#.##")} in {targetCurrency.Name}s ({targetCurrencyCode})";
    }
}
