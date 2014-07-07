using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarCenter.Models
{
    public class CarView
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public int Vintage { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string ProductionTime { get; set; }
        public string Condition { get; set; }
        public int PreviousOwners { get; set; }
        public string Site { get; set; }
        public int SiteId { get; set; }

        public CarView(Car c, Site s)
        {
            this.Id = c.Id;
            this.Brand = c.Brand;
            this.Condition = c.Condition;
            this.PreviousOwners = c.PreviousOwners;
            this.ProductionTime = c.ProductionTime.ToShortDateString();
            this.Site = s.ZipCode + "," + s.Address;
            this.Vintage = c.Vintage;
            this.Type = c.Type;
            this.SiteId = c.SiteId;
        }
    }
}