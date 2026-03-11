using Calculator.ViewModel;

namespace Calculator.View;

public partial class SinIntegralPage : ContentPage
{
	public SinIntegralPage(SinIntegralViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}