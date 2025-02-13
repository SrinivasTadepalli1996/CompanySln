using System.ComponentModel.DataAnnotations;

namespace CompanyApi.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Exchange { get; set; } = string.Empty;

        [Required]
        public string Ticker { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[A-Za-z]{2}[A-Za-z0-9]+$", ErrorMessage = "ISIN must start with two letters")]
        public string Isin { get; set; } = string.Empty;

        public string? Website { get; set; }
    }
}
