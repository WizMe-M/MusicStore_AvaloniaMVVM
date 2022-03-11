using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using MusicStore_AvaloniaMVVM.ViewModels;
using ReactiveUI;

namespace MusicStore_AvaloniaMVVM.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.WhenActivated(d => d(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async Task DoShowDialogAsync(InteractionContext<MusicStoreViewModel, AlbumViewModel?> interaction)
        {
            var dialog = new MusicStoreWindow();
            dialog.DataContext = interaction.Input;
            var result = await dialog.ShowDialog<AlbumViewModel?>(this);
            interaction.SetOutput(result);
        }
    }
}