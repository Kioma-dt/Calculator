using Calculator.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Calculator.Entities;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace Calculator.ViewModel
{
    public partial class HospitalViewModel : ObservableObject
    {
        IDbService _dbService;
        public ObservableCollection<HospitalWard> HospitalWards { get; } = new ObservableCollection<HospitalWard>();
        public ObservableCollection<Patient> Patients { get; } = new ObservableCollection<Patient>();

        public HospitalViewModel(IDbService dbService) 
        {
            _dbService = dbService;
        }

        [ObservableProperty]
        public partial bool Selected { get; set; } = true;

        [RelayCommand]
        public async Task InitHospitalWards()
        {

            await _dbService.InitAsync();
            HospitalWards.Clear();

            var wards = await _dbService.GetAllWardsAsync();

            foreach (var ward in wards)
            {
                HospitalWards.Add(ward);
            }
        }

        [RelayCommand]
        public async Task UpdatePatients(HospitalWard hospitalWard)
        {
            Selected = false;
            Patients.Clear();

            var dbPatients = await _dbService.GetPatientsInWardAsync(hospitalWard.Id);

            foreach (var pateint in dbPatients)
            {
                Patients.Add(pateint);
            }
        }
    }
}
