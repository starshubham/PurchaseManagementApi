using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Country
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
