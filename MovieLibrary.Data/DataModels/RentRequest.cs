using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Data.DataModels
{
    public class RentRequest
    {
        public List<int> Movies { get; set; }
        public string SelectedIDnumber { get; set; }
    }
}
