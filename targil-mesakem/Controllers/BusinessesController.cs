using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using targil_mesakem.Models;

namespace targil_mesakem.Controllers
{
    public class BusinessesController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            try
            {
                Businesses business = new Businesses();
                List<Businesses> bList = business.Read();
                return Request.CreateResponse(HttpStatusCode.OK, bList);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Businesses/cusine/{_cusine}")]
        public HttpResponseMessage Get(string _cusine)
        {
            try
            {
                Businesses business = new Businesses();
                List<Businesses> bListfiltered = new List<Businesses>();
                List<Businesses> bList = business.Read();

                for (int i = 0; i < bList.Count; i++)
                {
                    var ctgry = bList[i].Category.Split(',');//נהפך למערך במידה ויש יותר מקטגוריה אחת למסעדה
                    foreach (var item in ctgry)
                    {
                        if (_cusine == item || (" " + _cusine) == item)//בגלל הרווח שיש בטבלה של האסקיואל
                        {
                            bListfiltered.Add(bList[i]);
                        }
                    }

                }


                return Request.CreateResponse(HttpStatusCode.OK, bListfiltered);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
           
        }

        //לפי העדפות
        [HttpGet]
        [Route("api/Businesses/pre/{id}/cusine/{_cusine}")]
        public HttpResponseMessage Get(int id ,string _cusine)
        {
            try
            {
               
                Businesses business = new Businesses();
                List<Businesses> rlist = business.ReadPreRes(id);
                List<Businesses> rListfiltered = new List<Businesses>();
                for (int i = 0; i < rlist.Count; i++)
                {
                    var ctgry = rlist[i].Category.Split(',');
                    foreach (var item in ctgry)
                    {
                        if (_cusine == item || (" " + _cusine) == item)
                        {
                            rListfiltered.Add(rlist[i]);
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, rListfiltered);                              
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }

        }
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Businesses business)
        {
            try
            {
                business.Insert();
                return Request.CreateResponse(HttpStatusCode.Created, business);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}