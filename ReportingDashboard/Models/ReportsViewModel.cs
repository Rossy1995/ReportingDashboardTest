﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportingDashboard.Models
{
    public class ReportsViewModel
    {
        public string SearchText { get; set; }

        public List<usertime> UserTimes { get; set; }

        public List<user> Users { get; set; }
    }
}