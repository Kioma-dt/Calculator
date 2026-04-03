using Calculator.ViewModel;
using Calculator.Entities;

namespace Calculator.View;

public partial class HospitalPage : ContentPage
{
	public HospitalPage(HospitalViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

		WardPicker.SelectedIndex = 1;
	}
}