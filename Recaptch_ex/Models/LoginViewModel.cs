using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recaptch_ex.Models
{
    public class LoginViewModel
    {

        public string Email { get; set; }
        public string Password { get; set; }

        //برای نمایش  پیغام استفاده شده است
        public bool Message { get; set; }
    }
}