using Calculator.View;
namespace Calculator
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ConverterPage), typeof(ConverterPage));
        }
    }
}
