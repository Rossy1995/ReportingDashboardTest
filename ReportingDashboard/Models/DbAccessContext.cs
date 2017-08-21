using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ReportingDashboard.Models
{
    public class DbAccessContext : DbContext
    {
        public DbAccessContext(): base("GC") {}
        public DbSet <UserTime> UserTime
        {
            get;
            set;
        }
    }
}