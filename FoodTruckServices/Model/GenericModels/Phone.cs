using System.ComponentModel.DataAnnotations;

namespace FoodTruckServices.Model
{
    public class Phone
    {
        public int PhoneID { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = Constants.Errors.InvalidContactText)]
        public string PhoneNumber { get; set; }

        public ContactTypeEnum PhoneType { get; set; }

        public int DisplayOrder { get; set; }
    }
}