namespace Chirper.Models
{
    using System.Collections.Generic;

    public class Tag
    {
        public Tag()
        {
           this.Chirps = new HashSet<Chirp>();
        }

       public string TagRef { get; set; }

        public virtual ICollection<Chirp> Chirps { get; set; }
    }
}
