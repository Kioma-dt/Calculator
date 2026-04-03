using Calculator.Entities;

namespace Calculator.Services
{
    public interface IDbService
    {
        Task<IEnumerable<HospitalWard>> GetAllWardsAsync();
        Task<IEnumerable<Patient>> GetPatientsInWardAsync(int wardId);
        Task InitAsync();
    }
}
