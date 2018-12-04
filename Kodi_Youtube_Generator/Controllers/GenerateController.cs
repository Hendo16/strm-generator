using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Kodi_Youtube_Generator.Services;
using Kodi_Youtube_Generator.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Kodi_Youtube_Generator.Controllers
{
    public class GenerateController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public GenerateController(IHostingEnvironment hostService)
        {
            _hostingEnvironment = hostService;
        }
        [HttpGet]
        public async Task<IActionResult> Parse(HomeIndexViewModel nvm)
        {
            var service = new YoutubeParser();
            var results = await service.GetPlaylistID(nvm.playlist_link);
            HomeParseViewModel vm = new HomeParseViewModel 
            {
                video_list = results
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Parse(HomeParseViewModel vm) 
        {
            var wwwrootPath = _hostingEnvironment.WebRootPath;
            var ids = JsonConvert.DeserializeObject<List<string>>(vm.id_list);
            var plugin_base = "plugin://plugin.video.youtube/?path=/root/video&action=play_video&videoid=";
            for (int i = 1; i < ids.Count(); i++)
            {
                var full = Path.Combine(wwwrootPath, "temp", ("S01E0" + i + ".strm"));
                var logFile = System.IO.File.Create(full);
                var logWriter = new System.IO.StreamWriter(logFile);
                logWriter.WriteLine(plugin_base + ids[i]);
                logWriter.Dispose();
            }
            var startPath = Path.Combine(wwwrootPath, "temp");
            var zip = Path.Combine(wwwrootPath,"temp", "test.zip");
            ZipFile.CreateFromDirectory(startPath, zip, CompressionLevel.Fastest, false);
            return RedirectToAction("Index", "Home");
        }
    }
}