using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using App7.Services;
using App7.Models;
using Microsoft.Toolkit.Uwp.UI.Animations;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using System.Net.Http;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml;

namespace App7.Views
{
    public sealed partial class VideosPage : Page, INotifyPropertyChanged
    {

        VideoParameters VideoParameters = new VideoParameters();
        public ObservableCollection<SearchResults> Source { get; set; } = new ObservableCollection<SearchResults>();
        public List<SearchResults> SearchResult { get; set; }
        public VideosPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Wait, 0);

            base.OnNavigatedTo(e);

            if (e.Parameter is string)
            {
                VideoParameters.SearchQuery = e.Parameter.ToString();
                SearchResult = GetVideos.doStuff(VideoParameters.SearchQuery);

                if (SearchResult != null)
                {
                    foreach (var item in SearchResult)
                    {
                        Source.Add(item);
                    }

                    Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
                }

                if (SearchResult == null)
                {
                    // Create the message dialog and set its content
                    var messageDialog = new MessageDialog("No videos found. Sorry");
                    // Show the message dialog
                    await messageDialog.ShowAsync();
                    NavigationService.Navigate<MainPage>();
                }
            
            }

            if (e.Parameter == null)
            {
                // Create the message dialog and set its content
                var messageDialog = new MessageDialog("No search string available. Try again.");
                // Show the message dialog
                await messageDialog.ShowAsync();
                NavigationService.Navigate<MainPage>();
            }

          
        }

        private void OnItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is SearchResults item)
            {
                VideoParameters.VideoId = item.Id.VideoId;
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(item);
                NavigationService.Navigate<VideosDetailPage>(VideoParameters);
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
