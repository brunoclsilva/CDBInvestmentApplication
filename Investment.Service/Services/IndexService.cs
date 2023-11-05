using Investiment.Domain.Interfaces.Services;

namespace Investment.Service.Services
{
    public class IndexService : IIndexService
    {
        private const decimal TB = 1.08m; // 108%
        private const decimal CDI = 0.009m; // 0.9%
        public async Task<decimal> GetTb()
        {
            return await Task.FromResult(TB);
        }

        public async Task<decimal> GetCdi()
        {
            return await Task.FromResult(CDI);
        }
    }
}
