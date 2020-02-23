using App7.Models;
using App7.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace App7.Services
{
  public static class GetVideos
    {

        public static List<SearchResults>  doStuff(string Q, string pageToken = null)
        {
            List<SearchResults> SearchResult = new List<SearchResults>();
            try
            {
               SearchResult = Run(Q, pageToken);
                return SearchResult;
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    var messageDialog = new MessageDialog("Error: " + e.Message + " Please try again later");
                    messageDialog.ShowAsync();
                    NavigationService.Navigate<MainPage>();
                }
                return null;
            }
        }

        private static List<SearchResults> Run(string Q, string PageToken = null)
        {
            List<SearchResults> SearchResults = new List<SearchResults>();
            string ApiKey = "AIzaSyAUww0w3oUmmWfo6ncP4EGpSHlinhlQLtw";

            HttpClient httpClient = new HttpClient();

            if (PageToken == null)
            {
                PageToken = "";
            }

            HttpResponseMessage responseMessage = httpClient.GetAsync("https://www.googleapis.com/youtube/v3/search?part=snippet&maxResults=50&pageToken=" + PageToken + "&q=" + Q + "&key=" + ApiKey + "").Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                var response = responseMessage.Content.ReadAsAsync<YoutubeModel>();


               //string NextPageToken = response.Result.NextPageToken;
               //string PrevPageToken = response.Result.PrevPageToken;
                // Add each result to the appropriate list, and then display the lists of
                // matching videos, channels, and playlists.
                foreach (var searchResult in response.Result.Items)
                {
                    switch (searchResult.Id.Kind)
                    {
                        case "youtube#video":
                            SearchResults.Add(searchResult);
                            break;
                    }
                }
                return SearchResults;
            }
            else
            {
               var messageDialog = new MessageDialog(responseMessage.Content.ToString(), "Please try again later");
               messageDialog.ShowAsync();
                return null;
            }
        }

    }
}
