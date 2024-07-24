using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Common.Models
{
    public class State
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Country")]
        public string CountryCode { get; set; }

        [JsonIgnore]
        public Country Country { get; set; }
    }
}
