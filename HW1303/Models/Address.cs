using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HW1303.Models
{
    public class Address
    {
        [ValidateNever]
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Building_no { get; set; }
    }
}
