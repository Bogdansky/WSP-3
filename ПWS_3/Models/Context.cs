using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ПWS_3.Models
{
    public class Context : DbContext
    {
        public Context() : base("ConnectionString") { }

        public DbSet<Student> Students { get; set; }
    }
}