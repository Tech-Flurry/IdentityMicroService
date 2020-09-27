﻿namespace Domain.Models.Email
{
    public class EmailConfiguration
    {
        public string Server { get; set; }
        public int? Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Sender { get; set; }
    }
}
