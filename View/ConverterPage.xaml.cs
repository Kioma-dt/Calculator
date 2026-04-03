using Calculator.ViewModel;

namespace Calculator.View;

public partial class ConverterPage : ContentPage
{
	public ConverterPage(ConverterViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}