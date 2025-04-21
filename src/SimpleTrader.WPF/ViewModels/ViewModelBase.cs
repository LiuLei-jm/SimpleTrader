using SimpleTrader.WPF.Models;
using SimpleTrader.WPF.ViewModels.Factories;

namespace SimpleTrader.WPF.ViewModels
{
    public delegate TViewModel CreateViewModel<TViewModel>()
        where TViewModel : ViewModelBase;

    public class ViewModelBase : ObservableObject { }
}
