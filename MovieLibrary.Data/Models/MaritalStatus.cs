﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MovieLibrary.Data.Models
{
    public partial class MaritalStatus
    {
        public MaritalStatus()
        {
            Users = new HashSet<User>();
        }

        public int MaritalStatusId { get; set; }
        public string Caption { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}