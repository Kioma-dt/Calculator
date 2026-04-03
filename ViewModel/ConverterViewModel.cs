using Calculator.Services;
using Calculator.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Calculator.View;


namespace Calculator.ViewModel
{
    [QueryProperty("Rate", "Rate")]
    public partial class ConverterViewModel : ObservableObject
    {
        public ConverterViewModel() 
        {           
        }
        [ObservableProperty]
        public partial Rate Rate {  get; set; }
        [ObservableProperty]
        public partial string CurrencyAbreviation { get; set; }
        [ObservableProperty]
        public partial int CurrencyScale { get; set; }
        [ObservableProperty]
        public partial decimal CurrencyRate { get; set; }

        [ObservableProperty]
        public partial string FromCurrency {  get; set; }
        [ObservableProperty]
        public partial string ToCurrency { get; set; } = "BYN";

        [ObservableProperty]
        public partial string FromValueString {  get; set; } = String.Empty;
        [ObservableProperty]
        public partial string ToValueString { get; set; } = String.Empty;

        [ObservableProperty]
        public partial bool IsFromValueStringCorrect { get; set; } = true;

        [RelayCommand]
        void ChangeConvertDirection()
        {
            var temp = FromCurrency;
            FromCurrency = ToCurrency;
            ToCurrency = temp;

            this.Convert();
        }

        [RelayCommand]
        void Convert()
        {
            try
            {
                decimal fromValue = Decimal.Parse(FromValueString);
                IsFromValueStringCorrect = true;
                decimal toValue;

                if (ToCurrency == "BYN")
                {
                    toValue = fromValue / CurrencyScale * CurrencyRate;
                }
                else
                {
                    toValue = fromValue / CurrencyRate * CurrencyScale;
                }

                ToValueString = toValue.ToString();

            }
            catch (FormatException)
            {
                IsFromValueStringCorrect = false;

                if (FromValueString == String.Empty)
                {
                    ToValueString = String.Empty;
                }
            }
            catch (Exception)
            {

            }
        }

        [RelayCommand]
        async Task GoBackAsync()
        {
            var task = Shell.Current?.GoToAsync("..");

            if (task is not null) await task;
        }


        partial void OnRateChanged(Rate value)
        {
            //FromValueString = String.Empty;
            //ToValueString = String.Empty;
            CurrencyAbreviation = value.Cur_Abbreviation;
            CurrencyScale = value.Cur_Scale;
            FromCurrency = CurrencyAbreviation;
            //ToCurrency = "BYN";
            CurrencyRate = value.Cur_OfficialRate ?? 1;
        }
    }
}
