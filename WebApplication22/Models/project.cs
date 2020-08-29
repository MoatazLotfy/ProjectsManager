using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication22.Models
{
    public class project
    {
        [Key]
        public int projectid { set; get; }
        // [Required]
      //  [RegularExpression("[0-9]+")]
        public int price { set; get; }
        //  [Required]
        public String describtion { set; get; }
        public String projectname { set; get; }
        public String status { set; get; }
        public String startdate { set; get; }
        public String enddate { set; get; }
        public user Customer { get; set; }
        public user Manager { set; get; }
        public user leader { set; get; }
        ICollection<drrequest> requests { set; get; }
        ICollection<userrequest> userrequest { set; get; }

    }
}