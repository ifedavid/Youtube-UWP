using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using App7.Services;
using Windows.UI.Popups;


namespace App7.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {

        public MainPage()
        {
            InitializeComponent();
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);

        }


        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = SearchBox.Text;
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Wait, 0);


            if (searchQuery != null)
            {
                this.Frame.Navigate(typeof(VideosPage), searchQuery);
            }

            if (searchQuery == null)
            {
                // Create the message dialog and set its content
                var messageDialog = new MessageDialog("No search query. Please input a search Query");
                // Show the message dialog
                await messageDialog.ShowAsync();
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
