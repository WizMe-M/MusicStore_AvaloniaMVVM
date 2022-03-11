using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows.Input;
using MusicStore_AvaloniaMVVM.Models;
using ReactiveUI;

namespace MusicStore_AvaloniaMVVM.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            RxApp.MainThreadScheduler.Schedule(LoadAlbums);
            ShowDialog = new Interaction<MusicStoreViewModel, AlbumViewModel?>();

            BuyMusic = ReactiveCommand.CreateFromTask(async () =>
            {
                var store = new MusicStoreViewModel();

                var result = await ShowDialog.Handle(store);

                if (result is not null)
                {
                    Albums.Add(result);
                    await result.SaveToDiskAsync();
                }
            });
        }

        private ICommand BuyMusic { get; }
        public Interaction<MusicStoreViewModel, AlbumViewModel?> ShowDialog { get; }

        public ObservableCollection<AlbumViewModel> Albums { get; } = new();

        private async void LoadAlbums()
        {
            var albums = (await Album.LoadCachedAsync()).Select(x => new AlbumViewModel(x));
            foreach (var album in albums)
            {
                Albums.Add(album);
            }

            foreach (var album in Albums)
            {
                await album.LoadCover();
            }
        }
    }
}