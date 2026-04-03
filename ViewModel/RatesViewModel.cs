using Calculator.Services;
using Calculator.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Calculator.View;

namespace Calculator.ViewModel
{
    public partial class RatesViewModel : ObservableObject
    {
        readonly HashSet<String> _requestedRates = new HashSet<String>() {"RUB", "EUR", "USD", "CHF", "CNY", "GBP"};
        readonly IRateService _rateService;

        public ObservableCollection<Rate> Rates { get; } = new ObservableCollection<Rate>();

        public RatesViewModel(IRateService rateService)
        {
            _rateService = rateService;
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        [NotifyCanExecuteChangedFor(nameof(GetRatesCommand))]
        public partial bool IsBusy { get; set; } = false;

        public bool IsNotBusy => !IsBusy;

        [ObservableProperty]
        public partial DateTime Date { get; set; } = DateTime.Now;

        [RelayCommand]
        async Task GoToConverter(Rate rate)
        {
            if (rate is null) return;

            var task = Shell.Current?.GoToAsync(nameof(ConverterPage), false,
                new Dictionary<string, object>
                {
                    { "Rate", rate }
                });
            if (task is not null) await task;
        }

        public bool CanGetRate() => IsNotBusy;

        [RelayCommand(CanExecute = nameof(CanGetRate))]
        public async Task GetRates()
        {
            IsBusy = true;
            Rates.Clear();

            var webRates = await _rateService.GetRatesAsync(Date);

            webRates = webRates
                .Select(r => r)
                .Where(r => _requestedRates.Contains(r.Cur_Abbreviation))
                .ToList();

            foreach (var rate in webRates) 
            {
                Rates.Add(rate);
            }
            IsBusy = false;
        }
    }
}
