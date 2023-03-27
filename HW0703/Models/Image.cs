namespace HW0703.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

        public Image() 
        {
            Tags = new List<Tag>();
        }
    }
}
