using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HW1403.Models
{
    public class mpGood
    {
        static int counter = 0;
        [ValidateNever]
        public int Id { get; set; }
        public string Name { get; set; }
        public int MpCategoryId { get; set; }

        public mpGood() { Id = counter++; }
    }
}
