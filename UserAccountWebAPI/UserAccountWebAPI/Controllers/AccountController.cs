using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using UserAccountWebAPI.Models;
using System.Web.Http.Results;
namespace UserAccountWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountController : ApiController
    {
        [HttpGet]
        [Route("api/account/getall")]
        public IHttpActionResult getListAccount()
        {
            return Ok(Global.Data.UsersDB.getListUsers());
        }

        [HttpPost]
        [Route("api/account/login")]
        public IHttpActionResult checkLogin([FromBody]Users user)
        {
            Users result = Global.Data.UsersDB.checkLogin(user);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }


        [HttpPost]
        [Route("api/account/add")]
        public IHttpActionResult addAccount([FromBody]Users user)
        {
            int result = Global.Data.UsersDB.addAccount(user);
            if (result > 0)
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        [Route("api/account/update")]
        public IHttpActionResult updateAccount([FromBody]Users user)
        {
            int result = Global.Data.UsersDB.updateAccount(user);
            if (result > 0)
                return Ok();
            return BadRequest();
        }


        [HttpDelete]
        [Route("api/account/delete")]
        public IHttpActionResult deleteAccount([FromBody]Users user)
        {
            int result = Global.Data.UsersDB.deleteAccount(user);
            if (result > 0)
                return Ok();
            return BadRequest();
        }
    }
}
