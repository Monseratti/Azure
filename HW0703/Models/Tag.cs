namespace HW0703.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public string Value { get; set; }
        public virtual Image Image { get; set; }
    }
}
