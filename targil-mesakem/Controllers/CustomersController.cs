using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using targil_mesakem.Models;

namespace targil_mesakem.Controllers
{
    public class CustomersController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            try
            {
                Customers customer = new Customers();
                List<Customers> cList = customer.Read();
                return Request.CreateResponse(HttpStatusCode.OK, cList);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Customers/id/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Customers customer = new Customers();
                List<Customers> cList = customer.ReadCusHigh();
                foreach (var item in cList)
                {
                    if (id == item.Id)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, item); 
                    }
                }
                customer = null;
                return Request.CreateResponse(HttpStatusCode.OK, customer);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

       
            // POST api/<controller>
            public HttpResponseMessage Post([FromBody] Customers customer)
        {
            //ברישום הראשוני נגיע לאלס
            try
            {
                //להוסיף או לעדכן למשתמש העדפות
                if (customer.Fname == null)
                {
                    customer.InsertPreHigh();
                }
                else
                //לאתחל משתמש חדש
                {
                    customer.Insert();
                }
                
                return Request.CreateResponse(HttpStatusCode.Created, customer);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
      
        public HttpResponseMessage Delete(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, "deleted");
        }
    }
}