namespace WeddinsPlanner.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Present
    {
        [Key]
        public int Id { get; set; }
        
        public int InvitationId { get; set; }

        public Invitation Invitation { get; set; }

        [NotMapped]
        public Person Owner => this.Invitation.Guest;
    }
}
