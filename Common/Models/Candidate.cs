using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Common.Models
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string FatherName { get; set; }

        [Required]
        [StringLength(50)]
        public string MotherName { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        [StringLength(200)]
        public string PermanentAddress { get; set; }

        [Required]
        [StringLength(200)]
        public string CorrespondingAddress { get; set; }

        [Required]
        [StringLength(5)]
        [ForeignKey("Country")]
        public string CountryCode { get; set; }

        [Required]
        [ForeignKey("State")]
        public int StateId { get; set; }

        [Required]
        [ForeignKey("District")]
        public int DistrictId { get; set; }

        [Required]
        [MaxLength(6)]
        public int PinCode { get; set; }

        [Required]
        [StringLength(10)]
        public string MobileNo { get; set; }

        [Required]
        [ForeignKey("IDCardType")]
        public int IDCardTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string IDCardDetails { get; set; }

        [Required]
        public byte[] Photo { get; set; }

        // Navigation Properties
        /// <summary>
        /// [JsonIgnore] --> This is useful when you don't want certain properties to be included in the JSON payload during serialization
        /// </summary>
        [JsonIgnore]                            
        public Country Country { get; set; }
        [JsonIgnore]
        public State State { get; set; }
        [JsonIgnore]
        public District District { get; set; }
        [JsonIgnore]
        public IDCardType IDCardType { get; set; }
    }
}
