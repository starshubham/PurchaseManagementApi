using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class IDCardType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
