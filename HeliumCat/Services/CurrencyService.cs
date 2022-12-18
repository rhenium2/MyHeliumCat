using HeliumCat.Models;
using LocalObjectCache;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HeliumCat.Services;

public static class CurrencyService
{
    // Based on https://github.com/fawazahmed0/currency-api
    public static async Task<CurrencyRate> GetConversionRate(string fromCurrency, string toCurrency, DateTime dateTime)
    {
        var currencyRate = Cache.Default.GetOne<CurrencyRate>(x => x.BaseCurrency.Equals(fromCurrency) && 
                                                                   x.ExchangeCurrency.Equals(toCurrency) && 
                                                                   x.Date == dateTime.Date);
        if (currencyRate != null)
        {
            return currencyRate;
        }

        var uri = $"https://cdn.jsdelivr.net/gh/fawazahmed0/currency-api@1/{dateTime.ToString("yyyy-MM-dd")}/currencies/{fromCurrency}/{toCurrency}.json";
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        var jObject = JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());
        var rate = new CurrencyRate
        {
            Date = DateTime.Parse(jObject["date"].Value<string>()),
            BaseCurrency = fromCurrency,
            ExchangeCurrency = toCurrency,
            Rate = jObject[toCurrency].Value<decimal>()
        };
        Cache.Default.InsertOne(rate);

        return rate;
    }
}