﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MovieLibrary.Data.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Idnumber { get; set; }
        public int MaritalStatusId { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public virtual MaritalStatus MaritalStatus { get; set; }
        public ICollection<RentedMovie> RentedMovies { get; set; }
    }
}