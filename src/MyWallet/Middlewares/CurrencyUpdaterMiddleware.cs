using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using MyWallet.Services.DataTransferModels;
using MyWallet.Services.Services.Interfaces;

namespace MyWallet.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CurrencyUpdaterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IEntryService _entryService;

        public CurrencyUpdaterMiddleware(RequestDelegate next, IEntryService entryService)
        {
            _next = next;
            _entryService = entryService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                if (await NewConversionRatiosNeeded())
                {
                    await RequestNewConversionRatios();
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Exception occured when downloading ratios.");
            }
            
            await _next(httpContext);
        }

        private async Task<bool> NewConversionRatiosNeeded()
        {
            var ratios = await _entryService.GetAllConversionRatios();
            var newest = ratios.Max(x => x.Date);
            return newest.Date != DateTime.Today; //will shifted by day because CNB releases new info only on working days after 14:30
        }

        private async Task RequestNewConversionRatios()
        {
            string date = DateTime.Today.ToString("dd.MM.yyyy");
            var address = new Uri($"http://www.cnb.cz/cs/financni_trhy/devizovy_trh/kurzy_devizoveho_trhu/denni_kurz.txt?date={date}");
            var request = (HttpWebRequest)WebRequest.Create(address);

            var response = await request.GetResponseAsync();
            var stream = response.GetResponseStream();
            var result = await ParseResponseFile(stream);
            stream?.Dispose();

            if (result == null) return;

            SaveToDatabase(result);

        }

        private async void SaveToDatabase(IEnumerable<ConversionRatioDTO> result)
        {
            if (result==null) return;
            await _entryService.AddConversionRatios(result);
        }

        private async Task<IEnumerable<ConversionRatioDTO>> ParseResponseFile(Stream stream)
        {
            if (stream == null) return null;
            using (var reader = new StreamReader(stream))
            {
                var file = reader.ReadToEnd();
                var lines = file.Split(new []{'\n'},StringSplitOptions.RemoveEmptyEntries);

                if (lines.Length == 0)
                {
                    return null;
                }

                var ratios = lines.Skip(2).Select(x =>
                {
                    var splitted = x.Split('|');
                    var currency = splitted[3];
                    decimal ratio;
                    decimal baseR;
                    if (!decimal.TryParse(splitted[4], out ratio) || !decimal.TryParse(splitted[2], NumberStyles.Currency,CultureInfo.InvariantCulture,out baseR))
                    {
                        return null;
                    }

                    return new Tuple<string, decimal>(currency, decimal.Divide(ratio, baseR));
                });

                var currencies = await _entryService.GetAllCurrencies();
                var czk = currencies.SingleOrDefault(x => x.Code == "CZK") ?? new CurrencyDTO() {Code = "CZK"};

                var result = (from cur in currencies
                    join rat in ratios on cur.Code equals rat.Item1
                    select new ConversionRatioDTO()
                    {
                        CurrencyFrom = cur,
                        CurrencyTo = czk,
                        Ratio = rat.Item2,
                        Date = DateTime.Today,
                        Type = "CNB"
                    }).ToList();
                result.Add(new ConversionRatioDTO()
                {
                    CurrencyFrom = czk,
                    CurrencyTo = czk,
                    Ratio = 1m,
                    Date = DateTime.Today,
                    Type = "CNB",
                });
                return result;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CurrencyUpdaterMiddlewareExtensions
    {
        public static IApplicationBuilder UseCurrencyUpdaterMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CurrencyUpdaterMiddleware>();
        }
    }
}
