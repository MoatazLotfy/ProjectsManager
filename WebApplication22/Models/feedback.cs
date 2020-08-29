using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication22.Models
{
    public class feedback
    {
        public int id { set; get; }
        public String describtion { set; get; }
        public user Sender { set; get; }
        public user Reciver { set; get; }
        public user Trainee { set; get; }
        public project project { set; get; }
       
    }
}