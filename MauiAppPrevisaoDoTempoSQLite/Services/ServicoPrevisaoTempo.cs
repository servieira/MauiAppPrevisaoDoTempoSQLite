using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PrevisaoDoTempoApp.Models;
using MauiAppPrevisaoDoTempoSQLite.Data;
using MauiAppPrevisaoDoTempoSQLite.Models;
using System.Diagnostics;
using Newtonsoft.Json.Linq;


namespace MauiAppPrevisaoDoTempoSQLite.Services
{
    public class ServicoPrevisaoTempo
    {
        private readonly HttpClient _httpClient;
        private readonly string appId = "6135072afe7f6cec1537d5cb08a5a1a2";
        private readonly string baseUrl = "https://api.openweathermap.org/data/2.5/weather";
        private readonly PrevisaoTempoDatabase _database;

        public ServicoPrevisaoTempo(PrevisaoTempoDatabase database)
        {
            _httpClient = new HttpClient();
            _database = database;
        }

        public async Task<PrevisaoTempo> ObterPrevisao(string cidade)
        {
            var url = $"{baseUrl}?q={cidade}&units=metric&appid={appId}";
            var response = await _httpClient.GetStringAsync(url);

            if (!string.IsNullOrEmpty(response))
            {
                var rascunho = JObject.Parse(response);

                double temp_min = (double)rascunho["main"]["temp_min"];
                double temp_max = (double)rascunho["main"]["temp_max"];
                double temp = (double)rascunho["main"]["temp"];



                var previsao = JsonConvert.DeserializeObject<PrevisaoTempo>(response);


                if (previsao != null)
                {                    

                    int teste = await _database.SaveConsultaAsync(new PesquisaPrevisaoTempo
                    {
                        Cidade = cidade,
                        DataPesquisa = DateTime.Now,
                        Temperatura = temp
                    });

                    Debug.WriteLine("------------------------------------------");
                    Debug.WriteLine(teste);
                    Debug.WriteLine("------------------------------------------");

                    previsao.Cidade = cidade;
                    previsao.Data = DateTime.Now;
                    previsao.Temperatura = temp;
                }
                return previsao;
            }

            return null;
        }
    }
}