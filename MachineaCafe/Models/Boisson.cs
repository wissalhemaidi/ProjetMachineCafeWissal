using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MachineaCafe.Models
{
    public class Boisson
    {
        public int id { set; get; }
        public string lib_Boisson { set; get; }
        public IEnumerable<SelectListItem> ChoixList { get; set; }
     
    }
}