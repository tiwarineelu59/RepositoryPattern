using Dapper;
using DogWrapperApi.Context;
using DogWrapperApi.Contracts;
using DogWrapperApi.Entities;
using Newtonsoft.Json;


namespace DogWrapperApi.Repository
{
    public class BreedRepository : IBreedRepository
    {
        private readonly DapperContext _context;
        private readonly HttpClient client;
        private readonly IConfiguration configuration;
        private readonly ILogger<BreedRepository> _logger;

        public BreedRepository(DapperContext context, IConfiguration iConfig, ILogger<BreedRepository> logger)
        {
            _context = context;
            configuration = iConfig;
            string externalDogApiURL = configuration.GetValue<string>("ExternalDogApiBaseURL");
            client = new HttpClient()
            {
                BaseAddress = new Uri(externalDogApiURL)
            };
            _logger = logger;
        }

        public async Task<IEnumerable<Breed>> GetBreedByNameAsync(string breed)
        {
            try
            {
                var dog_image_path = await DogBreedExternalAPI(breed);
                var splitBreedSubreed = breed.Split(' ');
                var dog_breed = (splitBreedSubreed.Length > 1) ? splitBreedSubreed[1] + "-" + splitBreedSubreed[0] : breed;


                using (var connection = _context.CreateConnection())
                {

                    var parameters = new { breed_name = dog_breed.ToLower(), image_path = dog_image_path };
                    var apiResponse = await connection.QueryAsync<Breed>("usp_DogsBreed", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    return apiResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in BreedRepo Breed Method : {ex}");
                throw ex;

            }
        }

        private async Task<string> DogBreedExternalAPI(string breed)
        {

            string url = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(breed))
                {

                    var splitBreedSubreed = breed.Split(' ');
                    if (splitBreedSubreed.Length > 1)
                    {
                        url = string.Format("/api/breed/{0}/{1}/images/random", splitBreedSubreed[1].ToLower(), splitBreedSubreed[0].ToLower());
                    }
                    else
                    {
                        url = string.Format("/api/breed/{0}/images/random", breed.ToLower());
                    }
                }

                var response = await client.GetAsync(url);
                var result = string.Empty;
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    var dynamicObj = JsonConvert.DeserializeObject<dynamic>(stringResponse);
                    result = dynamicObj?.message;
                }
                else
                {
                    _logger.LogError($"Exception in BreedRepo DogBreedExternalAPI:Dog breed {response.ReasonPhrase}");
                     throw new HttpRequestException(response.ReasonPhrase);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in BreedRepo DogBreedExternalAPI: {ex}");
                throw ex;
            }
        }

    }
}