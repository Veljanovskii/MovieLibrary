﻿using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Data.DataModels
{
    public class EmployeesTotal
    {
        public List<Employee> Employees { get; set; }
        public int TotalEmployees { get; set; }
    }
}
