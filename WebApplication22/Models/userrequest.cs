using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication22.Models
{
    public class userrequest
    {
        public int id { set; get; }
       // [Required]
        public String request { set; get; }
        public String satuts { set; get; }
        public user sender { set; get; }
        public user reciver { set; get; }
        public project projects { set; get; }
    }
}