using System.Text.Json.Serialization;

namespace DogWrapperApi.Entities
{
    public class Breed
    {
        [JsonPropertyName("breed_id")]
        public int Breed_Id{ get; set; }

        [JsonPropertyName("breed_name")]
        public string Breed_Name { get; set; }

        [JsonPropertyName("image_path")]
        public string Image_Path { get; set; }
    }
}
