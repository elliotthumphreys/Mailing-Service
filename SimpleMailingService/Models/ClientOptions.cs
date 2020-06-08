using System.Collections.Generic;

namespace SimpleMailingService.Models
{
    public class ClientOptions
    {
        public const string ConfigurationName = "Clients";

        public Dictionary<string, Client> Values { get; set; }
    }

    public class Client
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSSL { get; set; }
        public Authentication Authentication { get; set; }
    }

    public class Authentication
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}