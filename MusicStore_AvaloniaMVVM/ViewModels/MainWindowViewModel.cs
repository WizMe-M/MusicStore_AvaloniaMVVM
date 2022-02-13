using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Windows.Input;
using MusicStore_AvaloniaMVVM.ViewModels;
using ReactiveUI;

namespace MusicStore_AvaloniaMVVM.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            BuyMusic = ReactiveCommand.Create(() =>
            {
            });
            
        }

        private ICommand BuyMusic { get; }
    }
}