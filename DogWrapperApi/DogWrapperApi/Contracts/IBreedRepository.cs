using DogWrapperApi.Entities;

namespace DogWrapperApi.Contracts
{
    public interface IBreedRepository
    {
        public Task<IEnumerable<Breed>> GetBreedByNameAsync(string breed); 

    }
}
