using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BH.viewmodel
{
    public class itemviewmodel
    {
        public string itemID { get; set; }
        public int categoryId { get; set; }
        public string itemName { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase imagePath { get; set; }
        public decimal itemPrice { get; set; }

        public IEnumerable<SelectListItem> categorySelectlistitem { get; set; }
    }
}