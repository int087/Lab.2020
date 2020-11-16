using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab.Demo.EF.APISpotify;
using Lab.Demo.EF.Logic;
using Lab.Demo.EF.Web.Models;

namespace Lab.Demo.EF.Web.Controllers
{
    public class APIController : Controller
    {
        // GET: API
        public ActionResult Index()
        {
            SpotifyView spotifyModel = new SpotifyView();
            List<ItemView> items = new List<ItemView>();

            spotifyModel.Items = items;

            return View(spotifyModel);
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<ActionResult> find(SpotifyView model)
        {
            SpotifyLogic client = new SpotifyLogic();

            var album = await client.GetAlbum(model.album.ToString(), model.artist.ToString());
            SpotifyView spotifyModel = new SpotifyView();
            List<ItemView> items = (from i in album.tracks.items
                                    orderby i.track_number
                                    select new ItemView
                                    {
                                        track_number = i.track_number,
                                        duration_ms = i.duration_ms,
                                        name = i.name
                                    }).ToList();

            spotifyModel.album = model.album;
            spotifyModel.artist = model.artist;
            spotifyModel.Items = items;

            return View("index", spotifyModel);
        }
    }
}