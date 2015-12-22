using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserAccountWebAPI.Models;

namespace UserAccountWebAPI
{
    public class Data
    {
        private AccountContext _UsersDB = new AccountContext();

        public AccountContext UsersDB
        {
            get { return _UsersDB; }
            set { _UsersDB = value; }
        }

    }
}