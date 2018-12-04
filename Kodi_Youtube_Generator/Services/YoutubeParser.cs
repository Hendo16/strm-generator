using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;

using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace Kodi_Youtube_Generator.Services
{
    public class YoutubeParser
    {
        public async Task<IList<PlaylistItem>> GetPlaylistID(string link)
        {
          UserCredential credential;
          using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
          {
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                // This OAuth 2.0 access scope allows for full read/write access to the
                // authenticated user's account.
                new[] { YouTubeService.Scope.Youtube },
                "user",
                CancellationToken.None,
                new FileDataStore(this.GetType().ToString())
            );
          }

          var youtubeService = new YouTubeService(new BaseClientService.Initializer()
          {
            HttpClientInitializer = credential,
            ApplicationName = this.GetType().ToString()
          });

            var uri = new Uri(link);
            var query = HttpUtility.ParseQueryString(uri.Query);
            var id = query["list"];
            var req = youtubeService.PlaylistItems.List("snippet");
            req.PlaylistId = id;
            var t = await req.ExecuteAsync();
            return t.Items;
        }

    }
}
