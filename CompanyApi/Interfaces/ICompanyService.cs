using CompanyApi.Models;

namespace CompanyApi.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();
        Task<Company?> GetCompanyByIdAsync(int id);
        Task<Company?> GetCompanyByIsinAsync(string isin);
        Task<Company> CreateCompanyAsync(Company company);
        Task UpdateCompanyAsync(int id, Company company);
        Task DeleteCompanyAsync(int id);
    }
}
