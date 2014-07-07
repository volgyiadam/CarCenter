using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarCenter.Models
{
    public class Car
    {

        private int _Id;

        public virtual int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _Brand;

        public virtual string Brand
        {
            get { return _Brand; }
            set { _Brand = value; }
        }

        private string _Type;

        public virtual string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private int _Vintage;

        public virtual int Vintage
        {
            get { return _Vintage; }
            set { _Vintage = value; }
        }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        private DateTime _ProductionTime;

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public virtual DateTime ProductionTime
        {
            get { return _ProductionTime; }
            set { _ProductionTime = value; }
        }

        private string _Condition;

        public virtual string Condition
        {
            get { return _Condition; }
            set { _Condition = value; }
        }

        private int _PreviousOwners;

        public virtual int PreviousOwners
        {
            get { return _PreviousOwners; }
            set { _PreviousOwners = value; }
        }

        private int _SiteId;

        public virtual int SiteId
        {
            get { return _SiteId; }
            set { _SiteId = value; }
        }





    }

}