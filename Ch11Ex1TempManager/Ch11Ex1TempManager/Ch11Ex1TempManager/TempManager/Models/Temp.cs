namespace TempManager.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;

    public class Temp
    {
        public int Id { get; set; }

        [Required]
        [Remote("CheckDate", "Validation")]
        public DateTime? Date { get; set; }

        [Required]
        [Range(-200, 200)]
        public double? Low { get; set; }

        [Required]
        [Range(-200, 200)]
        public double? High { get; set; }
    }
}
