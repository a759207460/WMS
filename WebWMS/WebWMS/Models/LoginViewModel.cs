﻿namespace WebWMS.Models
{
    public class LoginViewModel
    {
        public string Account { get; set; }

        public string PassWord { get; set; }

        public string ValidateCode { get; set; }
        public bool Remember { get; set; }
    }
}
