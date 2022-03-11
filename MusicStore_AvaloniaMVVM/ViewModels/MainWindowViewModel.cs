using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;

namespace MusicStore_AvaloniaMVVM.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            ShowDialog = new Interaction<MusicStoreViewModel, AlbumViewModel?>();
            
            BuyMusic = ReactiveCommand.CreateFromTask(async () =>
            {
                var store = new MusicStoreViewModel();

                var result = await ShowDialog.Handle(store);

                if (result is not null) 
                    Albums.Add(result);
            });
        }

        private ICommand BuyMusic { get; }
        public Interaction<MusicStoreViewModel, AlbumViewModel?> ShowDialog { get; }

        public ObservableCollection<AlbumViewModel> Albums { get; } = new();
    }
}