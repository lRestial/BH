using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BH.viewmodel
{
    public class shoppingviewmodel
    {
        public string itemID { get; set; }
        public string itemName { get; set; }
        public decimal itemPrice { get; set; }
        public string imagePath { get; set; }
        public string Description { get; set; }
        public string category { get; set; }

        public string searching { get; set; }
    }
}