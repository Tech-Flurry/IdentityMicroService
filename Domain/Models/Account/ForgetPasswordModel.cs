using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Account
{
    public class ForgetPasswordModel
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
