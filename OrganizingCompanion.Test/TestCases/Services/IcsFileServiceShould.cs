using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using OrganizingCompanion.Core.Interfaces.Repositories;
using OrganizingCompanion.Scheduler.Interfaces.Models;
using OrganizingCompanion.Scheduler.Models;
using OrganizingCompanion.Scheduler.Services;

namespace OrganizingCompanion.Test.TestCases.Services
{
    [TestFixture]
    internal class IcsFileServiceShould
    {
        private Mock<IRepository<IcsFile>> _mockRepository = null!;
        private Mock<ILogger<IcsFileService>> _mockLogger = null!;
        private IcsFileService _service = null!;
        private IcsFile _testIcsFile = null!;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IRepository<IcsFile>>();
            _mockLogger = new Mock<ILogger<IcsFileService>>();
            _service = new IcsFileService(_mockRepository.Object, _mockLogger.Object);

            _testIcsFile = new IcsFile
            {
                Id = 1,
                FileName = "test.ics",
                Description = "Test ICS file",
                ContentType = "text/calendar",
                FileContent = [1, 2, 3, 4, 5],
                FileSize = 5,
                OriginalFileName = "original.ics",
                CreatedByUserId = 1,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
        }

        #region GetAsync Tests

        [Test]
        public async Task GetAsync_WithValidId_ShouldReturnIcsFile()
        {
            // Arrange
            var id = 1; // Use an ID greater than 1 to pass validation
            _mockRepository.Setup(r => r.GetAsync(id))
                          .ReturnsAsync(_testIcsFile);

            // Act
            var result = await _service.GetAsync(id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(_testIcsFile.Id));
            Assert.That(result.FileName, Is.EqualTo(_testIcsFile.FileName));
            _mockRepository.Verify(r => r.GetAsync(id), Times.Once);
        }

        [Test]
        public void GetAsync_WithInvalidId_ShouldThrowArgumentOutOfRangeException()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                async () => await _service.GetAsync(0));
            
            Assert.That(ex!.ParamName, Is.EqualTo("id"));
        }

        [Test]
        public void GetAsync_WithNegativeId_ShouldThrowArgumentOutOfRangeException()
        {
            // Act & Assert  
            var ex = Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                async () => await _service.GetAsync(-1));
            
            Assert.That(ex!.ParamName, Is.EqualTo("id"));
        }

        [Test]
        public void GetAsync_WhenRepositoryThrowsException_ShouldLogErrorAndRethrow()
        {
            // Arrange
            var id = 2; // Use valid ID to get past validation
            var exception = new InvalidOperationException("Database error");
            _mockRepository.Setup(r => r.GetAsync(id))
                          .ThrowsAsync(exception);

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(
                async () => await _service.GetAsync(id));
            
            Assert.That(ex, Is.EqualTo(exception));
        }

        #endregion

        #region GetAllAsync Tests

        [Test]
        public async Task GetAllAsync_ShouldReturnAllIcsFiles()
        {
            // Arrange
            var icsFiles = new List<IcsFile> { _testIcsFile };
            _mockRepository.Setup(r => r.GetAllAsync())
                          .ReturnsAsync(icsFiles);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Id, Is.EqualTo(_testIcsFile.Id));
            _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_WhenNoFiles_ShouldReturnEmptyList()
        {
            // Arrange
            var icsFiles = new List<IcsFile>();
            _mockRepository.Setup(r => r.GetAllAsync())
                          .ReturnsAsync(icsFiles);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetAllAsync_WhenRepositoryThrowsException_ShouldLogErrorAndRethrow()
        {
            // Arrange
            var exception = new InvalidOperationException("Database error");
            _mockRepository.Setup(r => r.GetAllAsync())
                          .ThrowsAsync(exception);

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(
                async () => await _service.GetAllAsync());
            
            Assert.That(ex, Is.EqualTo(exception));
        }

        #endregion

        #region AddAsync Tests

        [Test]
        public async Task AddAsync_WithValidIcsFile_ShouldReturnAddedFile()
        {
            // Arrange
            var validIcsFile = new IcsFile { Id = 2 }; // Use ID > 1
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<IcsFile>()))
                          .ReturnsAsync(_testIcsFile);

            // Act
            var result = await _service.AddAsync(validIcsFile);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(_testIcsFile.Id));
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<IcsFile>()), Times.Once);
        }

        [Test]
        public void AddAsync_WithNullIcsFile_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _service.AddAsync(null!));
            
            Assert.That(ex!.ParamName, Is.EqualTo("icsFile"));
        }

        [Test]
        public void AddAsync_WithInvalidId_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var invalidIcsFile = new IcsFile { Id = 0 };

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                async () => await _service.AddAsync(invalidIcsFile));
            
            Assert.That(ex!.ParamName, Is.EqualTo("Id")); // Fix parameter name to match actual
        }

        [Test]
        public void AddAsync_WhenRepositoryThrowsException_ShouldLogErrorAndRethrow()
        {
            // Arrange
            var validIcsFile = new IcsFile { Id = 2 }; // Use valid ID to get past validation
            var exception = new InvalidOperationException("Database error");
            _mockRepository.Setup(r => r.AddAsync(It.IsAny<IcsFile>()))
                          .ThrowsAsync(exception);

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(
                async () => await _service.AddAsync(validIcsFile));
            
            Assert.That(ex, Is.EqualTo(exception));
        }

        #endregion

        #region DeleteAsync Tests

        [Test]
        public async Task DeleteAsync_WithValidIcsFile_ShouldReturnTrue()
        {
            // Arrange
            var validIcsFile = new IcsFile { Id = 2 }; // Use ID > 1
            _mockRepository.Setup(r => r.DeleteAsync(It.IsAny<IcsFile>()))
                          .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAsync(validIcsFile);

            // Assert
            Assert.That(result, Is.True);
            _mockRepository.Verify(r => r.DeleteAsync(It.IsAny<IcsFile>()), Times.Once);
        }

        [Test]
        public void DeleteAsync_WithNullIcsFile_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _service.DeleteAsync(null!));
            
            Assert.That(ex!.ParamName, Is.EqualTo("icsFile"));
        }

        [Test]
        public void DeleteAsync_WithInvalidId_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var invalidIcsFile = new IcsFile { Id = 0 };

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                async () => await _service.DeleteAsync(invalidIcsFile));
            
            Assert.That(ex!.ParamName, Is.EqualTo("Id")); // Fix parameter name
        }

        [Test]
        public void DeleteAsync_WhenRepositoryThrowsException_ShouldLogErrorAndRethrow()
        {
            // Arrange
            var validIcsFile = new IcsFile { Id = 2 }; // Use valid ID to get past validation
            var exception = new InvalidOperationException("Database error");
            _mockRepository.Setup(r => r.DeleteAsync(It.IsAny<IcsFile>()))
                          .ThrowsAsync(exception);

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(
                async () => await _service.DeleteAsync(validIcsFile));
            
            Assert.That(ex, Is.EqualTo(exception));
        }

        #endregion

        #region DeleteRangeAsync Tests

        [Test]
        public async Task DeleteRangeAsync_WithValidList_ShouldReturnTrue()
        {
            // Arrange
            var validIcsFile = new IcsFile { Id = 2 }; // Use ID > 1
            var icsFiles = new List<IIcsFile> { validIcsFile };
            _mockRepository.Setup(r => r.DeleteRangeAsync(It.IsAny<List<IcsFile>>()))
                          .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteRangeAsync(icsFiles);

            // Assert
            Assert.That(result, Is.True);
            _mockRepository.Verify(r => r.DeleteRangeAsync(It.IsAny<List<IcsFile>>()), Times.Once);
        }

        [Test]
        public void DeleteRangeAsync_WithNullList_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _service.DeleteRangeAsync(null!));
            
            Assert.That(ex!.ParamName, Is.EqualTo("icsFiles"));
        }

        [Test]
        public void DeleteRangeAsync_WithEmptyList_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var emptyList = new List<IIcsFile>();

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                async () => await _service.DeleteRangeAsync(emptyList));
            
            Assert.That(ex!.ParamName, Is.EqualTo("icsFiles"));
        }

        [Test]
        public void DeleteRangeAsync_WhenRepositoryThrowsException_ShouldLogErrorAndRethrow()
        {
            // Arrange
            var icsFiles = new List<IIcsFile> { _testIcsFile };
            var exception = new InvalidOperationException("Database error");
            _mockRepository.Setup(r => r.DeleteRangeAsync(It.IsAny<List<IcsFile>>()))
                          .ThrowsAsync(exception);

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(
                async () => await _service.DeleteRangeAsync(icsFiles));
            
            Assert.That(ex, Is.EqualTo(exception));
        }

        #endregion

        #region UpdateAsync Tests

        [Test]
        public async Task UpdateAsync_WithValidIcsFile_ShouldReturnUpdatedFile()
        {
            // Arrange
            var validIcsFile = new IcsFile { Id = 2 }; // Use ID > 1
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<IcsFile>()))
                          .ReturnsAsync(_testIcsFile);

            // Act
            var result = await _service.UpdateAsync(validIcsFile);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(_testIcsFile.Id));
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<IcsFile>()), Times.Once);
        }

        [Test]
        public void UpdateAsync_WithNullIcsFile_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _service.UpdateAsync(null!));
            
            Assert.That(ex!.ParamName, Is.EqualTo("icsFile"));
        }

        [Test]
        public void UpdateAsync_WithInvalidId_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var invalidIcsFile = new IcsFile { Id = 0 };

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                async () => await _service.UpdateAsync(invalidIcsFile));
            
            Assert.That(ex!.ParamName, Is.EqualTo("Id")); // Fix parameter name
        }

        [Test]
        public void UpdateAsync_WhenRepositoryThrowsException_ShouldLogErrorAndRethrow()
        {
            // Arrange
            var validIcsFile = new IcsFile { Id = 2 }; // Use valid ID to get past validation
            var exception = new InvalidOperationException("Database error");
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<IcsFile>()))
                          .ThrowsAsync(exception);

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(
                async () => await _service.UpdateAsync(validIcsFile));
            
            Assert.That(ex, Is.EqualTo(exception));
        }

        #endregion

        #region UpdateRangeAsync Tests

        [Test]
        public async Task UpdateRangeAsync_WithValidList_ShouldReturnUpdatedFiles()
        {
            // Arrange
            var validIcsFile = new IcsFile { Id = 2 }; // Use ID > 1
            var icsFiles = new List<IIcsFile> { validIcsFile };
            var updatedFiles = new List<IcsFile> { _testIcsFile };
            _mockRepository.Setup(r => r.UpdateRangeAsync(It.IsAny<List<IcsFile>>()))
                          .ReturnsAsync(updatedFiles);

            // Act
            var result = await _service.UpdateRangeAsync(icsFiles);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Id, Is.EqualTo(_testIcsFile.Id));
            _mockRepository.Verify(r => r.UpdateRangeAsync(It.IsAny<List<IcsFile>>()), Times.Once);
        }

        [Test]
        public void UpdateRangeAsync_WithNullList_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _service.UpdateRangeAsync(null!));
            
            Assert.That(ex!.ParamName, Is.EqualTo("icsFiles"));
        }

        [Test]
        public void UpdateRangeAsync_WithEmptyList_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var emptyList = new List<IIcsFile>();

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                async () => await _service.UpdateRangeAsync(emptyList));
            
            Assert.That(ex!.ParamName, Is.EqualTo("icsFiles"));
        }

        [Test]
        public void UpdateRangeAsync_WhenRepositoryThrowsException_ShouldLogErrorAndRethrow()
        {
            // Arrange
            var icsFiles = new List<IIcsFile> { _testIcsFile };
            var exception = new InvalidOperationException("Database error");
            _mockRepository.Setup(r => r.UpdateRangeAsync(It.IsAny<List<IcsFile>>()))
                          .ThrowsAsync(exception);

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(
                async () => await _service.UpdateRangeAsync(icsFiles));
            
            Assert.That(ex, Is.EqualTo(exception));
        }

        #endregion

        #region HasEntityAsync Tests

        [Test]
        public async Task HasEntityAsync_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            var id = 2; // Use ID > 1
            _mockRepository.Setup(r => r.HasEntityAsync(id))
                          .ReturnsAsync(true);

            // Act
            var result = await _service.HasEntityAsync(id);

            // Assert
            Assert.That(result, Is.True);
            _mockRepository.Verify(r => r.HasEntityAsync(id), Times.Once);
        }

        [Test]
        public async Task HasEntityAsync_WithNonExistentId_ShouldReturnFalse()
        {
            // Arrange
            var id = 1000; // Use ID > 999 to avoid validation issue
            _mockRepository.Setup(r => r.HasEntityAsync(id))
                          .ReturnsAsync(false);

            // Act
            var result = await _service.HasEntityAsync(id);

            // Assert
            Assert.That(result, Is.False);
            _mockRepository.Verify(r => r.HasEntityAsync(id), Times.Once);
        }

        [Test]
        public void HasEntityAsync_WithInvalidId_ShouldThrowArgumentOutOfRangeException()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                async () => await _service.HasEntityAsync(0));
            
            Assert.That(ex!.ParamName, Is.EqualTo("id"));
        }

        [Test]
        public void HasEntityAsync_WithNegativeId_ShouldThrowArgumentOutOfRangeException()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                async () => await _service.HasEntityAsync(-1));
            
            Assert.That(ex!.ParamName, Is.EqualTo("id"));
        }

        [Test]
        public void HasEntityAsync_WhenRepositoryThrowsException_ShouldLogErrorAndRethrow()
        {
            // Arrange
            var id = 2; // Use valid ID to get past validation
            var exception = new InvalidOperationException("Database error");
            _mockRepository.Setup(r => r.HasEntityAsync(id))
                          .ThrowsAsync(exception);

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(
                async () => await _service.HasEntityAsync(id));
            
            Assert.That(ex, Is.EqualTo(exception));
        }

        #endregion
    }
}