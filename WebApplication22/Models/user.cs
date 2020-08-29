using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication22.Models
{
    public class user
    {
        [Key]
        public int userid { set; get; }
        //[Required]
        // [RegularExpression("[a-zA-Z]{1,}")]
        public String firstname { set; get; }
        //[Required]
        //[RegularExpression("[a-zA-Z]{1,}")]
        public String lastname { set; get; }
        //[Required]
        //[EmailAddress]
        public String Email { set; get; }
        //[Required]
      //  [DataType(DataType.Password)]
        //[RegularExpression("[a-zA-Z0-9]{6,14}")]
        public String password { set; get; }
       // [Required]
        //[RegularExpression("[0-9]{1,11}")]
        public String phone { set; get; }
        //[Required]
        public String status { set; get; }
        public String photo { set; get; }
        ICollection<project> projects { set; get; }
        ICollection<drrequest> requests { set; get; }
        ICollection<feedback> feedbacks { set; get; }
        ICollection<userrequest> userrequest { set; get; }
    }
}