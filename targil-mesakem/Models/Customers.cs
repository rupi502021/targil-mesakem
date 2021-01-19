using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using targil_mesakem.Models.DAL;

namespace targil_mesakem.Models
{
    public class Customers
    {
        int id;
        string fname;
        string lname;
        string email;
        string phone;
        string password;
        string image;
        int price_range;
        string[] highlights;

        public Customers(int id, string fname, string lname, string email, string phone, string password, string image, int price_range, string[] highlights)
        {
            Id = id;
            Fname = fname;
            Lname = lname;
            Email = email;
            Phone = phone;
            Password = password;
            Image = image;
            Price_range = price_range;
            Highlights = highlights;
        }

        public int Id { get => id; set => id = value; }
        public string Fname { get => fname; set => fname = value; }
        public string Lname { get => lname; set => lname = value; }
        public string Email { get => email; set => email = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Password { get => password; set => password = value; }
        public string Image { get => image; set => image = value; }
        public int Price_range { get => price_range; set => price_range = value; }
        public string[] Highlights { get => highlights; set => highlights = value; }

        public Customers() { }
        public void Insert()
        {
            DBServices dbs = new DBServices();
            dbs.Insert(this);
        }
        public void InsertPreHigh()
        {
            DBServices dbs = new DBServices();
            List<Highlight> hlist = new List<Highlight>();
            hlist = dbs.Gethighlights();
            dbs.InsertPreHigh(this,hlist);
        }
        public List<Customers> Read()
        {
            DBServices dbs = new DBServices();
            List<Customers> c = dbs.getcustomers();
            return c;
        }
        public List<Customers> ReadCusHigh()
        {
            DBServices dbs = new DBServices();
            List<Customers> c = dbs.getcustomersHigh();
            return c;
        }

        

    }
}