using CompanyApi.Interfaces;
using CompanyApi.Models;
using CompanyApi.Repositories;

namespace CompanyApi.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;

        public CompanyService(ICompanyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Company?> GetCompanyByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Company?> GetCompanyByIsinAsync(string isin)
        {
            return await _repository.GetByIsinAsync(isin);
        }

        public async Task<Company> CreateCompanyAsync(Company company)
        {
            if (await _repository.GetByIsinAsync(company.Isin) != null)
                throw new InvalidOperationException("A company with this ISIN already exists.");

            return await _repository.AddAsync(company);
        }

        public async Task UpdateCompanyAsync(int id, Company company)
        {           
            try
            {
                if (id != company.Id)
                    throw new ArgumentException("ID mismatch.");

                var existingCompany = await _repository.GetByIdAsync(id);
                if (existingCompany == null)
                    throw new KeyNotFoundException($"Company with ID {id} not found.");

                // ✅ Ensure database entity is updated correctly
                existingCompany.Name = company.Name;
                existingCompany.Exchange = company.Exchange;
                existingCompany.Ticker = company.Ticker;
                existingCompany.Isin = company.Isin;
                existingCompany.Website = company.Website;
                await _repository.UpdateAsync(existingCompany);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] UpdateCompanyAsync: {ex.Message}");
                throw;
            }
        }


        public async Task DeleteCompanyAsync(int id)
        {
            var existingCompany = await _repository.GetByIdAsync(id);
            if (existingCompany == null)
                throw new KeyNotFoundException("Company not found.");

            await _repository.DeleteAsync(id);
        }
    }
}
