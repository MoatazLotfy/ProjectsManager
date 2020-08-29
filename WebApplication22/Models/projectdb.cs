using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication22.Models
{
    public class projectdb : DbContext
    {
        public projectdb()
        {
            Database.SetInitializer<projectdb>(new 

MigrateDatabaseToLatestVersion<projectdb,WebApplication22.Migrations.Configuration>());
        }
     public   DbSet<user> usertable { set; get; }
      public   DbSet<project> projecttable { set;get;}
      public  DbSet<workson> worksontable { set; get; }
      public  DbSet<feedback> feedbacktable { set; get; }
       public  DbSet<userrequest> urequesttable { set; get; }
      public  DbSet<drrequest> drrequesttable { set; get; }
      


    }
}