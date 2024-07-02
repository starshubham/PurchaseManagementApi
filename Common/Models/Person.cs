using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Person
    {
        [Key]
        public int id { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public DateOnly dateOfBirth { get; set; } = new DateOnly();
        public DateTime creationDate { get; set; } = DateTime.Now;
        public string gender { get; set; }
        public string salutation { get; set; }
        public string mobNo { get; set; }
        public string address { get; set; }
    }
}
