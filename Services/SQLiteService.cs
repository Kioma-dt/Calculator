using Calculator.Entities;
using SQLite;

namespace Calculator.Services
{
    public class SQLiteService : IDbService
    {
        string _dbFileName = "HospitalDb";
        SQLiteConnection _db;
        IRandomService _randomService;

        public SQLiteService(IRandomService randomService)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, _dbFileName);
            _db = new SQLiteConnection(dbPath);
            _randomService = randomService;
        }
        public async Task<IEnumerable<HospitalWard>> GetAllWardsAsync()
        {
            return await Task.Run(() =>_db.Table<HospitalWard>()
                                .ToList());
        }

        public async Task<IEnumerable<Patient>> GetPatientsInWardAsync(int wardId)
        {
            return await Task.Run( () => _db.Table<Patient>()
                .Where(p => p.HospitalWardId == wardId)
                .ToList());
        }

        public async Task InitAsync()
        {
            await Task.Run(() =>
            {
                var info = _db.GetTableInfo("HospitalWards");

                if (info.Count > 0)
                {
                    _db.DeleteAll<HospitalWard>();
                }

                info = _db.GetTableInfo("Patients");

                if (info.Count > 0)
                {
                    _db.DeleteAll<Patient>();
                }
                _db.CreateTable<HospitalWard>();
                _db.CreateTable<Patient>();

                for (int i = 0; i < 4; i++)
                {
                    var hospitalWard = new HospitalWard()
                    {
                        PersonInChargeName = _randomService.GetRandomName(),
                        IsVIP = _randomService.GetRandomBool(),
                        DroppersNumber = _randomService.GetRandomInt(1, 16)
                    };

                    _db.Insert(hospitalWard);

                    for (int j = 0; j < _randomService.GetRandomInt(5, 11); j++)
                    {
                        _db.Insert(new Patient()
                        {
                            Name = _randomService.GetRandomName(),
                            Age = _randomService.GetRandomInt(1, 100),
                            Diagnoses = _randomService.GetRandomDiagnose(),
                            HospitalWardId = hospitalWard.Id
                        });
                    }
                }
            });
           
        }
    }
}
