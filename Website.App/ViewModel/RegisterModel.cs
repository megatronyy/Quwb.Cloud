using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Website.App.ViewModel
{
    public class RegisterModel : BaseViewModel
    {
        public string Mobile { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public int CheckCode { get; set; }
    }
}
