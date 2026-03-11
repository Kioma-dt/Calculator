using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.ViewModel
{
    public partial class SinIntegralViewModel : ObservableObject
    {
        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        public event Action<double>? ProgressChanged;
        public event Action<double>? EvaluationCompleted;
        public SinIntegralViewModel()
        {
            ProgressChanged += x => Progress = x;
            EvaluationCompleted += x => { State = $"Result: {x}"; Progress = 1; IsBusy = false; };
            State = "Welcome to .NET MAUI!";
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        [NotifyCanExecuteChangedFor(nameof(CalculateCommand))]
        [NotifyCanExecuteChangedFor(nameof(StopCommand))]
        public partial bool IsBusy { get; set; }

        public bool IsNotBusy => !IsBusy;

        [ObservableProperty]
        public partial double Progress { get; set; }

        [ObservableProperty]
        public partial string State {  get; set; }

        public bool CanStart()
        {
            return IsNotBusy;
        }
        public bool CanStop()
        {
            return IsBusy;
        }

        [RelayCommand(CanExecute = nameof(CanStart))]
        async Task Calculate()
        {
            State = "Calculating...";
            IsBusy = true;
            var token = _cancellationTokenSource.Token;

                double result = await Task.Run(() =>
                {
                    int temp;
                    int iterationsCount = 0;
                    int iterationsToChangeProgress = 100000;


                    double localResult = 0;
                    for (double x = 0; x < 1; x += 1e-8)
                    {
                        if (token.IsCancellationRequested)
                        {
                            return 0;
                        }
                        localResult += Math.Sin(x) * 1e-8;

                        //for (int i = 0; i < 10; i++)
                        //{
                        //    temp = 2 * 2;
                        //}

                        if (++iterationsCount > iterationsToChangeProgress)
                        {
                            ProgressChanged?.Invoke(x);
                            iterationsCount = 0;
                        }
                    }

                    return localResult;
                }, token);

                if (token.IsCancellationRequested)
                {
                    IsBusy = false;
                    return;
                }
                EvaluationCompleted?.Invoke(result);
        }

        [RelayCommand(CanExecute = nameof(CanStop))]
        public async Task Stop()
        {
            await Task.Run(() =>
            {
                _cancellationTokenSource.Cancel();
                State = "Task Cancled!";
                _cancellationTokenSource = new CancellationTokenSource();
                IsBusy = false;
            });
        }
    }


}
