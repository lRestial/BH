using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BH.viewmodel
{
    public class manageloginviewmodel
    {
        [DisplayName("帳號")]
        [Required(ErrorMessage = "帳號不可空白")]
        public string managename { get; set; }

        [DisplayName("密碼")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "密碼不可空白")]
        public string managepassword { get; set; }

        public string loginerrormessage { get; set; }
    }
}