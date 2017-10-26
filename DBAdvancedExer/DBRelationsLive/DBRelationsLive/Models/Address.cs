namespace DBRelationsLive.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Address
    {
        [Key]
        [ForeignKey("Student")]
        public int Id { get; set; }

        public string Text { get; set; }

        public virtual Student Student { get; set; }
               
        public virtual Town Town { get; set; }

       
    }
}
