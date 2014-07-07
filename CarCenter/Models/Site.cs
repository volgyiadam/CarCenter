using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarCenter.Models
{
    public class Site
    {
        private int _Id;

        public virtual int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private int _ZipCode;

        public virtual int ZipCode
        {
            get { return _ZipCode; }
            set { _ZipCode = value; }
        }

        private string _Address;

        public virtual string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private int _ParkSlotCount;

        public virtual int ParkSlotCount
        {
            get { return _ParkSlotCount; }
            set { _ParkSlotCount = value; }
        }

        private int _ReservedCount;

        public virtual int ReservedCount
        {
            get { return _ReservedCount; }
            set { _ReservedCount = value; }
        }





    }
}