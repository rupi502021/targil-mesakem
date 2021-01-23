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
        [Route("api/Campaigns/cusine/{_cusine}")]
        public HttpResponseMessage GetStatusCamp(string _cusine)
        {
            try
            {
                Businesses business = new Businesses();
                Campaign campaign = new Campaign();
                List<Businesses> bListfiltered = new List<Businesses>();
                List<Campaign> cList = campaign.ActiveCamp();
                List<Businesses> bList = business.Read();
                for (int j = 0; j < cList.Count; j++)
                {
                    for (int i = 0; i < bList.Count; i++)
                    {
                        if (bList[i].Id == cList[j].Id)
                        {
                            var ctgry = bList[i].Category.Split(',');
                            foreach (var item in ctgry)
                            {
                                if (_cusine == item || (" " + _cusine) == item)
                                {
                                    bListfiltered.Add(bList[i]);
                                }
                            }
                        }
                    }
                }
               
                return Request.CreateResponse(HttpStatusCode.OK, bListfiltered);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Campaign campaign)
        {
            try
            {
                campaign.Insert();
                return Request.CreateResponse(HttpStatusCode.Created, campaign);
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
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "campaign: " + c + " does not exist");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, cList);
            }
        }
        [HttpPut]
        [Route("api/Campaigns/view")]
        public HttpResponseMessage Put([FromBody] List<Businesses> bList)//מקבל רשימה של מסעדות שיש להן קמפיין פעיל
        {
            var num=0;
            Businesses business = new Businesses();
            Campaign c = new Campaign();
            List<Businesses> bListfiltered = new List<Businesses>();
            List<Campaign> cList = c.ReadAll();
            for (int j = 0; j < cList.Count; j++)
            {
                 for (int i = 0; i < bList.Count; i++)
                 {
                    if(bList[i].Id == cList[j].Id)
                    {
                        cList[j].View += 1;
                        cList[j].Income += 0.1;
                        num += cList[j].Update();//סופר כמה רשומות התעדכנו
                        bListfiltered.Add(bList[i]);
                    }                      
                 }               
            }
            
            if (num == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "campaign not updated, no campaign with such cusine");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, bListfiltered);
            }
        }
        [HttpPut]
        [Route("api/Campaigns/click")]
        public HttpResponseMessage Put([FromBody] int id)
        {
            var num = 0;
            Campaign c = new Campaign();
            List<Campaign> cList = c.ReadAll();
            for (int j = 0; j < cList.Count; j++)
            {
                if (id == cList[j].Id)
                {
                    cList[j].Click += 1;
                    cList[j].Income += 0.5;
                    num += cList[j].Update();
                }
            }
            if (num == 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "campaign not updated, no campaign with such cusine");
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