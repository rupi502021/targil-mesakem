using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using targil_mesakem.Models.DAL;

namespace targil_mesakem.Models
{

    public class Businesses
    {
        int id;
        string photo_url;
        string name;
        double rating;
        string category;
        int price_range;
        string address;
        string phones;
        string url;
        string[] highlights;

        

        public Businesses(int id, string photo_url, string name, double rating, string category, int price_range, string address, string phones, string url, string[] highlights)
        {
            Id = id;
            Photo_url = photo_url;
            Name = name;
            Rating = rating;
            Category = category;
            Price_range = price_range;
            Address = address;
            Phones = phones;
            Url = url;
            Highlights = highlights;
        }

        public int Id { get => id; set => id = value; }
        public string Photo_url { get => photo_url; set => photo_url = value; }
        public string Name { get => name; set => name = value; }
        public double Rating { get => rating; set => rating = value; }
        public string Category { get => category; set => category = value; }
        public int Price_range { get => price_range; set => price_range = value; }
        public string Address { get => address; set => address = value; }
        public string Phones { get => phones; set => phones = value; }
        public string Url { get => url; set => url = value; }
        public string[] Highlights { get => highlights; set => highlights = value; }

        public Businesses() { }
        public void Insert()
        {
            DBServices dbs = new DBServices();
            
            List<Highlight> hlist = new List<Highlight>();
            hlist = dbs.Gethighlights();
            dbs.Insert(this);
            dbs.InsertResHigh(this, hlist);

        }

        public List<Businesses> Read()
        {
            DBServices dbs = new DBServices();
            List<Businesses> l = dbs.getRestaurants();
            return l;
        }
        public List<Businesses> ReadPreRes(int id_cus)
        {
            DBServices dbs = new DBServices();
            List<Businesses> l = dbs.getPreRes(id_cus);
            return l;
        }
        



    }
}