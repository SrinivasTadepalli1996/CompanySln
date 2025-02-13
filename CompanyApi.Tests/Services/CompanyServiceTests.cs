using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using FluentAssertions;
using CompanyApi.Controllers;
using CompanyApi.Interfaces;
using CompanyApi.Models;

public class CompanyServiceTests
{
    private readonly Mock<ICompanyService> _mockService;
    private readonly CompanyController _controller;

    public CompanyServiceTests()
    {
        _mockService = new Mock<ICompanyService>();
        _controller = new CompanyController(_mockService.Object);
    }

    [Fact]
    public async Task GetCompanies_ShouldReturnOk()
    {   
    // Arrange
        var companies = new List<Company>
        {
          new() { Id = 1, Name = "Test Co", Exchange = "NASDAQ", Ticker = "TST", Isin = "US12345678" }
        };

        _mockService.Setup(service => service.GetAllCompaniesAsync()).ReturnsAsync(companies);

        // Act
        var result = await _controller.GetCompanies();

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
    }


    [Fact]
public async Task GetCompanyById_ExistingId_ShouldReturnOk()
{
    // Arrange
    var company = new Company { Id = 1, Name = "Test Co", Exchange = "NASDAQ", Ticker = "TST", Isin = "US12345678" };

    _mockService.Setup(service => service.GetCompanyByIdAsync(1)).ReturnsAsync(company);

    // Act
    var result = await _controller.GetCompanyById(1);

    // Assert
    result.Result.Should().BeOfType<OkObjectResult>();
}


    [Fact]
public async Task GetCompanyById_NonExistingId_ShouldReturnNotFound()
{
    // Arrange
    _mockService.Setup(service => service.GetCompanyByIdAsync(99)).ReturnsAsync((Company)null);

    // Act
    var result = await _controller.GetCompanyById(99);

    // Assert
    result.Result.Should().BeOfType<NotFoundObjectResult>();
}


    [Fact]
public async Task AddCompany_ValidCompany_ShouldReturnCreated()
{
    // Arrange
    var company = new Company { Id = 1, Name = "New Co", Exchange = "NYSE", Ticker = "NEW", Isin = "US98765432" };

    _mockService.Setup(service => service.CreateCompanyAsync(It.IsAny<Company>())).ReturnsAsync(company);

    // Act
    var result = await _controller.CreateCompany(company);

    // Assert
    result.Result.Should().BeOfType<CreatedAtActionResult>();
}


    [Fact]
    public async Task UpdateCompany_ValidCompany_ShouldReturnNoContent()
    {
        // Arrange
        var company = new Company { Id = 1, Name = "Updated Co", Exchange = "NYSE", Ticker = "UPD", Isin = "US98765432" };

        _mockService.Setup(service => service.UpdateCompanyAsync(1, company)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateCompany(1, company);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task UpdateCompany_NonMatchingId_ShouldReturnBadRequest()
    {
        // Arrange
        var company = new Company { Id = 2, Name = "Mismatch", Exchange = "NASDAQ", Ticker = "MM", Isin = "US11223344" };

        _mockService.Setup(service => service.UpdateCompanyAsync(1, company)).ThrowsAsync(new ArgumentException("ID mismatch."));

        // Act
        var result = await _controller.UpdateCompany(1, company);

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.Value.Should().BeEquivalentTo(new { message = "ID mismatch." });
    }

    [Fact]
    public async Task UpdateCompany_NonExistingCompany_ShouldReturnNotFound()
    {
        // Arrange
        var company = new Company { Id = 99, Name = "Non-existing", Exchange = "NASDAQ", Ticker = "NA", Isin = "US55667788" };

        _mockService.Setup(service => service.UpdateCompanyAsync(99, company)).ThrowsAsync(new KeyNotFoundException("Company not found."));

        // Act
        var result = await _controller.UpdateCompany(99, company);

        // Assert
        var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
        notFoundResult.Value.Should().BeEquivalentTo(new { message = "Company not found." });
    }

    [Fact]
    public async Task DeleteCompany_ExistingId_ShouldReturnNoContent()
    {
        // Arrange
        _mockService.Setup(service => service.DeleteCompanyAsync(1)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteCompany(1);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeleteCompany_NonExistingId_ShouldReturnNotFound()
    {
        // Arrange
        _mockService.Setup(service => service.DeleteCompanyAsync(99)).ThrowsAsync(new KeyNotFoundException("Company not found."));

        // Act
        var result = await _controller.DeleteCompany(99);

        // Assert
        var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
        notFoundResult.Value.Should().BeEquivalentTo(new { message = "Company not found." });
    }
}
