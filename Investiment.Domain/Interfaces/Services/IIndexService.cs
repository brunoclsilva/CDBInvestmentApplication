namespace Investiment.Domain.Interfaces.Services
{
    public interface IIndexService
    {
        Task<decimal> GetTb();
        Task<decimal> GetCdi();
    }
}
