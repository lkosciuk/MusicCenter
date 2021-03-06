﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Dal.EntityModels
{
    public class Concert
        : BaseEntity
    {
        public DateTime date { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public virtual ICollection<Files> images { get; set; }
        public virtual ICollection<Favourites> favourites { get; set; }
        public int? TourID { get; set; }
        public virtual Tour tour { get; set; }
        public int BandID { get; set; }
        public virtual Band ConcertOwner { get; set; }
        public virtual ICollection<Band> bands { get; set; }

        public Concert()
        {
            images = new List<Files>();
            favourites = new List<Favourites>();
            bands = new List<Band>();
        }
    }
}
