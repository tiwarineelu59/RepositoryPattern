using Newtonsoft.Json;

namespace DogAPI
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter Dog Breed");
            var dog_breed_name = Console.ReadLine();

            if (string.IsNullOrEmpty(dog_breed_name))
            {
                Console.WriteLine("Enter Dog Breed");
            }
            else
            {
                Program.DogBreedExternalAPI(dog_breed_name.Trim());

            }
        }



        private static void DogBreedExternalAPI(string dog_breed_name)
        {
]            try
            {
                var client = new HttpClient()
                {
                    BaseAddress = new Uri("https://localhost:7287/") //Wrapper Api
                };
                var url = string.Format("/api/breed/{0}", dog_breed_name);
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = response.Content.ReadAsStringAsync();
                    var dynamicObj = JsonConvert.DeserializeObject<dynamic>(stringResponse.Result);
                    Console.WriteLine("Dog Breed: " + dog_breed_name);
                    Console.WriteLine("Dog Image: " + dynamicObj[0]?.image_path);


                }
                else
                {
                    throw new HttpRequestException(response.ReasonPhrase);
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
