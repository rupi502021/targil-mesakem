using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using targil_mesakem.Models;

namespace targil_mesakem.Controllers
{
    public class CampaignsController : ApiController
    {
        // GET api/<controller>

        [HttpGet]
        [Route("api/Campaigns/camp")]
        public List<Campaign> GetCamp()
        {
            Campaign campaign = new Campaign();
            List<Campaign> cList = campaign.ReadAll();
            return cList;
        }

        [HttpGet]
        [Route("api/Campaigns/status")]
        public List<Campaign> GetStatusCamp()
        {
            Campaign campaign = new Campaign();
            return campaign.NonActive();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Campaign campaing)
        {
            try
            {
                campaing.Insert();
                return Request.CreateResponse(HttpStatusCode.Created, campaing);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody]Campaign c)
        {
            int num = c.Update();
            List<Campaign> cList = c.ReadAll();
            if (num == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "campaing: " + c + " does not exist");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, cList);
            }
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            Campaign c = new Campaign();
            int num = c.Delete(id);
            List<Campaign> cList = c.ReadAll();
            if (num == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "id: " + id + " does not exist");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, cList);
            }
        }
    }
}