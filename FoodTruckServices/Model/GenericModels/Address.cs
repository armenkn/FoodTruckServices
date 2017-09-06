using System.ComponentModel.DataAnnotations;

namespace FoodTruckServices.Model
{
    public class Address
    {
        public int AddressID { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 5, ErrorMessage = Constants.Errors.InvalidAddress1)]
        public string Address1 { get; set; }

        [StringLength(250, ErrorMessage = Constants.Errors.InvalidAddress2)]
        public string Address2 { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 2, ErrorMessage = Constants.Errors.InvalidCity)]
        public string City { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 5, ErrorMessage = Constants.Errors.InvalidZipcode)]
        public string Zipcode { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = Constants.Errors.InvalidState)]
        public string State { get; set; }

        public Coordination Coordination{ get; set; }
        
        public AddressTypeEnum AddressType { get; set; }
    }
}