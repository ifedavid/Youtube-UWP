using System;
using System.ComponentModel;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using App7.Core.Services;
using App7.Services;
using App7.Models;

using Microsoft.Toolkit.Uwp.UI.Animations;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

namespace App7.Views
{
    public sealed partial class VideosDetailPage : Page, INotifyPropertyChanged
    {
        private SearchResults Item { get; set; }

        private string VideoUrl { get; set; }

        public VideosDetailPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Wait, 0);
            base.OnNavigatedTo(e);
            var parameters = (VideoParameters)e.Parameter;

             VideoUrl = "https://youtube.com/embed/"+parameters.VideoId;
           
            if (parameters.VideoId != null && parameters.SearchQuery != null)
            {
                var data = GetVideos.doStuff(parameters.SearchQuery);
                Item = data.First(id => id.Id.VideoId == parameters.VideoId);
                Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
            }
            else
            {
                var messageDialog = new MessageDialog("No video found. Please try again");
                await messageDialog.ShowAsync();
                NavigationService.Navigate<MainPage>();
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(Item);
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
