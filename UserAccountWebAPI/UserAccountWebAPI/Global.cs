using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserAccountWebAPI
{
    public class Global
    {
        private static Data _Data = new Data();

        public static Data Data
        {
            get { return Global._Data; }
            set { Global._Data = value; }
        }
    }
}