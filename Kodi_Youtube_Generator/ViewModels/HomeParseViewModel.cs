using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodi_Youtube_Generator.ViewModels
{
    public class HomeParseViewModel
    {
        public IList<PlaylistItem> video_list { get; set; }
        public string id_list { get; set; }
    }
}
