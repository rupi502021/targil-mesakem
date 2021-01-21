using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using targil_mesakem.Models.DAL;

namespace targil_mesakem.Models
{
    public class Campaign
    {
        int id;
        double investment;
        double income;
        int view;
        int click;
        bool status;

        public Campaign(int id, double investment, double income, int view, int click, bool status)
        {
            Id = id;
            Investment = investment;
            Income = income;
            View = view;
            Click = click;
            Status = status;
        }

        public int Id { get => id; set => id = value; }
        public double Investment { get => investment; set => investment = value; }
        public double Income { get => income; set => income = value; }
        public int View { get => view; set => view = value; }
        public int Click { get => click; set => click = value; }
        public bool Status { get => status; set => status = value; }

        public Campaign() { }

        public void Insert()
        {
            DBServices dbs = new DBServices();
            dbs.Insert(this);
        }

        public int Delete(int id)
        {
            DBServices dbs = new DBServices();
            return dbs.Delete(id);
        }

        public int Update()
        {
            DBServices dbs = new DBServices();
            return dbs.Update(this);
        }

        public List<Campaign> ReadAll()
        {
            DBServices dbs = new DBServices();
            List<Campaign> c = dbs.getallcamp();
            return c;
        }

        public List<Campaign> ActiveCamp()
        {

            List<Campaign> cList = new List<Campaign>();
            DBServices dbs = new DBServices();
            dbs = dbs.getCampaignDT();
            dbs.dt = checkActiveCamp(dbs.dt);
            dbs.Update();

            foreach (DataRow dr in dbs.dt.Rows)
            {
                Campaign c = new Campaign();
                              
                c.Id = Convert.ToInt32(dr["RestaurantID"]);
                c.Investment = Convert.ToDouble(dr["Investment"]);
                c.Income = Convert.ToDouble(dr["Income"]);
                c.View = Convert.ToInt32(dr["Show"]);
                c.Click = Convert.ToInt32(dr["Click"]);
                c.Status = Convert.ToBoolean(dr["Active"]);
                if (c.Status != false)
                {
                    cList.Add(c);
                }
               
            }
            
            return cList;
        }

        private DataTable checkActiveCamp(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                double investment = Convert.ToDouble(dr["Investment"]);
                double income = Convert.ToDouble(dr["Income"]);
                bool active = Convert.ToBoolean(dr["Active"]);
                double profit = investment - income;
                if (profit <= 0)
                {
                    active = false;
                }
                dr["Active"] = active;
            }
            return dt;
        }

    }
}