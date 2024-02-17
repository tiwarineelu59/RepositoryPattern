namespace DogApiTests.Controller
{
    public class BreedControllerTests
    {
        [Fact]
        public async Task GetDogBreed_ValidBreed_ReturnsOk()
        {
            // Arrange
            string breedName = "rottweiler";
            var expectedBreedInfo = new Breed { Name = breedName, Description = "Strong and loyal breed." };

            var breedRepositoryMock = new Mock<IBreedRepository>();
            breedRepositoryMock.Setup(repo => repo.GetBreedByNameAsync(breedName)).ReturnsAsync(expectedBreedInfo);

            var loggerMock = new Mock<ILogger<BreedController>>();

            var controller = new BreedController(breedRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await controller.GetDogBreed(breedName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<Breed>(okResult.Value);
            Assert.Equal(expectedBreedInfo, model);
        }

        [Fact]
        public async Task GetDogBreed_NullBreed_ReturnsBadRequest()
        {
            // Arrange
            string breedName = null;

            var breedRepositoryMock = new Mock<IBreedRepository>();
            var loggerMock = new Mock<ILogger<BreedController>>();

            var controller = new BreedController(breedRepositoryMock.Object, loggerMock.Object);

            // Act
            var result = await controller.GetDogBreed(breedName);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Breed parameter is required.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetDogBreed_BreedNotFound_ReturnsNotFound()
        {
            // Arrange
            string breedName = "labrador";

            var breedRepositoryMock = new Mock<IBreedRepository>();
            breedRepositoryMock.Setup(repo => repo.GetBreedByNameAsync(breedName)).ReturnsAsync((Breed)null);

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