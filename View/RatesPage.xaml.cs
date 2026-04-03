using Calculator.ViewModel;

namespace Calculator.View;

public partial class RatesPage : ContentPage
{
	public RatesPage(RatesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

		DateEntry.MaximumDate = DateTime.Today;
	}
}