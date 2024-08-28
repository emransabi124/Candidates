using Candidates.Controllers;
using Candidates.Repositories.Dto;
using Candidates.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Threading.Tasks;

namespace TestCandidates
{
    public class CandidatesControllerTests
    {
        private readonly CandidatesController _controller;
        private readonly Mock<ICandidateRepository> _mockCandidateRepository;

        public CandidatesControllerTests()
        {
            // Create a mock of ICandidateRepository
            _mockCandidateRepository = new Mock<ICandidateRepository>();

            // Inject the mock repository into the controller
            _controller = new CandidatesController(_mockCandidateRepository.Object);
        }

        [Fact]
        public async Task AddOrUpdateCandidateWhenModelIsValid_ReturnsOkResult()
        {
            // Arrange
            var candidateDto = new CandidateDto
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                Comment = "Experienced developer.",
                PhoneNumber = "1234567890",
                GitHubProfileUrl = "http://localhost:5000/api/candidates/cache/john.doe@example.com",
                LinkedInProfileUrl = "http://localhost:5000/api/candidates/cache/john.doe@example.com"
            };

            var candidateOutputDto = new CandidateOutputDto
            {
                Success = true,
                Message = "Candidate created/updated successfully."
            };

            // Setup the mock repository to return a successful response
            _mockCandidateRepository.Setup(repo => repo.CreateOrEditCandidateAsync(It.IsAny<CandidateDto>()))
                    .ReturnsAsync(candidateOutputDto);  // Correct return type with expected result

            // Act
            var result = await _controller.AddOrUpdateCandidate(candidateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);  // Expecting OkObjectResult
            Assert.Equal(candidateOutputDto.ResultSave, okResult.Value);  // Check that the returned object matches the expected result
        }

        [Fact]
        public async Task AddOrUpdateCandidateWhenModelIsInvalid_ReturnsBadRequest()
        {
            // Arrange

            var candidateDto = new CandidateDto
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                Comment = "Experienced developer.",
                PhoneNumber = "1234567890"
            };

            // Simulate invalid model state
            _controller.ModelState.AddModelError("FirstName", "Required");

            // Act
            var result = await _controller.AddOrUpdateCandidate(candidateDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        // Additional test methods can go here...
    }
}
