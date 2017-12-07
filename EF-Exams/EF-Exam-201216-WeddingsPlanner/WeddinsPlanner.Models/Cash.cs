namespace WeddinsPlanner.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Cash:Present
    {
        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Amount { get; set; }
    }
}
