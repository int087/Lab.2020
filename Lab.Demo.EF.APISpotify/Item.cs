﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Demo.EF.APISpotify
{
    public class Item
    {
        public Item() { }
        public Album album { get; set; }
        public List<Artist> artists { get; set; }
        public int disc_number { get; set; }
        public int duration_ms { get; set; }
        public bool @explicit { get; set; }
        public ExternalIds external_ids { get; set; }
        public ExternalUrls external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public bool is_local { get; set; }
        public bool is_playable { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public object preview_url { get; set; }
        public int track_number { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }
}
