using Investiment.Domain.Entities;
using Investiment.Domain.Interfaces.Services;

namespace Investment.Service.Services
{
    public class CdbService : ICdbService
    {
        private readonly IIndexService _indexService;
        public CdbService(IIndexService indexService) 
        {
            _indexService = indexService;
        }

        public async Task<InvestmentResponse> CalculateInvestment(InvestmentRequest request)
        {
            if (request.InitialValue <= 0 || request.Months <= 1)
            {
                throw new ArgumentException("O valor inicial deve ser positivo e o prazo em meses deve ser maior que 1.");
            }

            decimal currentValue = request.InitialValue;
            decimal grossResult = 0;
            var tb = await _indexService.GetTb();
            var cdi = await _indexService.GetCdi();
            decimal monthlyCDI = cdi * tb;

            for (int i = 0; i < request.Months; i++)
            {
                var monthGain = currentValue * monthlyCDI;
                grossResult += monthGain;
                currentValue += grossResult;
            }

            decimal taxRate;
            if (request.Months <= 6)
            {
                taxRate = 0.225m; // 22.5%
            }
            else if (request.Months <= 12)
            {
                taxRate = 0.20m; // 20%
            }
            else if (request.Months <= 24)
            {
                taxRate = 0.175m; // 17.5%
            }
            else
            {
                taxRate = 0.15m; // 15%
            }

            decimal taxes = grossResult * taxRate;
            decimal netResult = grossResult - taxes;

            return new InvestmentResponse { GrossResult = Decimal.Parse(grossResult.ToString("0.00")), NetResult = Decimal.Parse(netResult.ToString("0.00")) };
        }
    }
}
