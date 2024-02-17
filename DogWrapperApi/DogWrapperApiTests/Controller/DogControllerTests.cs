using DogWrapperApi.Contracts;
using DogWrapperApi.Controllers;
using DogWrapperApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace DogWrapperApiTests.Controller
{
    public class BreedControllerTests
    {
        [Fact]
        public async Task GetDogBreed_ValidBreed_ReturnsOk()
        {
            // Arrange
            string breedName = "giant schnauzer";
            var expectedBreedInfo = new Breed { Breed_Id=17, Breed_Name = breedName, Image_Path = "https://images.dog.ceo/breeds/schnauzer-giant/n02097130_5432.jpg" };
            List<Breed> breedInfo= new List<Breed>();
            breedInfo.Add(expectedBreedInfo);

            var breedRepositoryMock = new Mock<IBreedRepository>();
            breedRepositoryMock.Setup(repo => repo.GetBreedByNameAsync(breedName)).Returns(Task.FromResult<IEnumerable<Breed>>(breedInfo));

            var loggerMock = new Mock<ILogger<BreedController>>();

            var controller = new BreedController(breedRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await controller.GetDogBreed(breedName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Breed>>(okResult.Value);
            Assert.Equal(breedInfo, model);
        }

      
        [Fact]
        public async Task GetDogBreed_BreedNotFound_ReturnsNotFound()
        {
            // Arrange
            string breedName = "labrador";

            var breedRepositoryMock = new Mock<IBreedRepository>();
            breedRepositoryMock.Setup(repo => repo.GetBreedByNameAsync(breedName)).Returns(Task.FromResult<IEnumerable<Breed>>(null));

            var loggerMock = new Mock<ILogger<BreedController>>();

            var controller = new BreedController(breedRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await controller.GetDogBreed(breedName);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetDogBreed_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            string breedName = "goldenretriever";

            var breedRepositoryMock = new Mock<IBreedRepository>();
            breedRepositoryMock.Setup(repo => repo.GetBreedByNameAsync(breedName)).ThrowsAsync(new Exception("Simulated exception"));

            var loggerMock = new Mock<ILogger<BreedController>>();

            var controller = new BreedController(breedRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await controller.GetDogBreed(breedName);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}