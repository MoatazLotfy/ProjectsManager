using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication22.Models
{
    public class drrequest
    {
        public int id { set; get; }
       // [Required]
        public String request { set; get; }
        public String status { set; get; }
        public user Sender { set; get; }
        public user reciver { set; get; }
        public int price { set; get; }
        public String startdate { set; get; }
        public String enddate { set; get; }

        public project projects { set; get; }
    }
}