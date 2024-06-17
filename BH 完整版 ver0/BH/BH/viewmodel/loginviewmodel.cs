using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BH.viewmodel
{
    public class loginviewmodel
    {
        [DisplayName("帳號")]
        [StringLength(12, ErrorMessage = "帳號錯誤", MinimumLength = 3)]
        [Required(ErrorMessage = "帳號不可空白")]
        public string username { get; set; }



        [DisplayName("密碼")]
        [DataType(DataType.Password)]
        [StringLength(16, ErrorMessage = "密碼錯誤", MinimumLength = 8)]
        [Required(ErrorMessage = "密碼不可空白")]
        public string password { get; set; }


        public string loginerrormessage { get; set; }



    }
}