using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HW1403.Models
{
    public class mpCategory
    {
        static int counter = 0;
        [ValidateNever]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<mpGood> mpGoods { get; set; }

        public mpCategory() 
        {
            Id = counter++;
            mpGoods = new List<mpGood>();
        }
    }
}
