using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab.Demo.EF.Web.Models
{
    public class SpotifyView
    {
        public string album { get; set; }
        public string artist { get; set; }
        public List<ItemView> Items { get; set; }
    }
}