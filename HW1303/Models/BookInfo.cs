using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HW1303.Models
{
    public class BookInfo
    {
        [ValidateNever]
        public int Id { get; set; }
        public string Book_title { get; set; }
        public string Authors { get; set; }
        public string Publishing_name { get; set; }
        public int Publishing_address_id { get; set; }
        public DateTime publishing_date { get; set; }
    }
}
