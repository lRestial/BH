using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BH.viewmodel
{
    public class accountcreateviewmodel
    {
        [DisplayName("帳號")]
        [StringLength(12, ErrorMessage = "帳號需3個字以上,12個字以下", MinimumLength = 3)]
        [Remote("IsUserExists", "login", ErrorMessage = "帳號已存在")]
        public string username { get; set; }

        [DisplayName("密碼")]
        [StringLength(16, ErrorMessage = "密碼長度為8到16", MinimumLength = 8)]       
        [DataType(DataType.Password)]
        public string password { get; set; }

        [DisplayName("電話")]
        [Remote("IsUserExists", "login", ErrorMessage = "電話已存在")]
        [Phone(ErrorMessage = "電話錯誤")]
        [DataType(DataType.PhoneNumber)]
        public int phone { get; set; }

        [DisplayName("email")]
        [Remote("IsUserExists", "login", ErrorMessage = "email已存在")]
        [EmailAddress(ErrorMessage = "email錯誤")]
        public string email { get; set; }

        [DisplayName(" address")]
        public string address { get; set; }


    }
}