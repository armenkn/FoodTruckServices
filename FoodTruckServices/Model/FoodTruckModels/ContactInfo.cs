using System.ComponentModel.DataAnnotations;

namespace FoodTruckServices.Model
{
    public class ContactInfo
    {
        public int ContactInfoID { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = Constants.Errors.InvalidContactText)]
        public string Contact { get; set; }

        public ContactTypeEnum ContactType { get; set; }

        public int DisplayOrder { get; set; }
    }
}