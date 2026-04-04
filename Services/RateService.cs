using Calculator.Entities;
using System.Net.Http.Json;
using System.Text.Json;
namespace Calculator.Services
{
    public interface IRateService
    {
        Task<IEnumerable<Rate>> GetRatesAsync(DateTime date);
    }

    public class RateService : IRateService
    {
        HttpClient _httpClient;
        public RateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Rate>> GetRatesAsync(DateTime date)
        {
            try
            {
                var parametars = $"?ondate={date.Date.ToString("yyyy-MM-dd")}&periodicity=0";
                var rates = await _httpClient.GetFromJsonAsync<IEnumerable<Rate>>(parametars,
                                                                new JsonSerializerOptions(JsonSerializerDefaults.Web));

                if (rates is null)
                {
                    throw new NullReferenceException("Rates are Null!");
                }

                return rates;
            }
            catch (System.Net.Http.HttpRequestException){
                throw;
            }
            catch (NullReferenceException)
            {
                return Enumerable.Empty<Rate>();
            }
        }
    }
}
