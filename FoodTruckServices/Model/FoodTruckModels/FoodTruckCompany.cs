using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodTruckServices.Model
{
    public class FoodTruckCompany
    {
        public int FoodTruckCompanyId { get; set; }

        public PersonalInfo OwnerInfo { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = Constants.Errors.InvalidBusinessName)]
        public string BusinessName { get; set; }

        public CompanyTypeEnum CompanyType { get; set;}

        [StringLength(100, MinimumLength = 2, ErrorMessage = Constants.Errors.InvalidBusinessName)]
        public string PermitNumber { get; set; }
        
        public virtual List<CuisineCategoryEnum> CuisineCategories { get; set; }

        [Required]
        public virtual List<Location> OfficeLocations { get; set; }

        [Required]
        public virtual List<ContactInfo> Contacts { get; set; }

        [Required]
        public virtual List<Phone> PhoneNumbers { get; set; }

        [Required]
        public virtual List<MealTypeEnum> MealTypes { get; set; }
        
        
        public bool HasVegeterianFood { get; set; }

        public bool HasVigenFood { get; set; }

        public bool HasCatering { get; set; }

        [StringLength(100, ErrorMessage = Constants.Errors.InvalidHealthCode)]
        public string HealthCode { get; set; }

        public string Description { get; set; }

        public List<string> AreaOfOperation { get; set; }
        
        [Required]
        public virtual List<FoodTruck> FoodTrucks { get; set; }

        public List<object> AdditionalInfo { get; set; }

    }
}
