using Investiment.Domain.Entities;

namespace Investiment.Domain.Interfaces.Services
{
    public interface ICdbService
    {
        public Task<InvestmentResponse> CalculateInvestment(InvestmentRequest request);
    }
}
