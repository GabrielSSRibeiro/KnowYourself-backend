using System;

namespace server.Models
{
    public class SignData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FDetails { get; set; }
        public string MDetails { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
    }
}