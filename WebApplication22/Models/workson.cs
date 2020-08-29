using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication22.Models
{
    public class workson
    {
        public int id { set; get; }
        public user trainee { set; get; }
        public project workignproject { set; get; }
    }
}