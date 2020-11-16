using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab.Demo.EF.Web.Models
{
    public partial class ItemView
    {
        public int track_number { get; set; }
        public string name { get; set; }
        public int duration_ms { get; set; }
    }
}