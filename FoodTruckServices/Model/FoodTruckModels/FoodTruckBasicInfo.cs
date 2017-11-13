using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.Model
{
    public class FoodTruckBasicInfo
    {
        public int FoodTruckID { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = Constants.Errors.InvalidTruckMake)]
        public string TruckMake { get; set; }

        [StringLength(10, MinimumLength = 4, ErrorMessage = Constants.Errors.InvalidLicensePlate)]
        public string LicensePlate { get; set; }

        [StringLength(100, ErrorMessage = Constants.Errors.InvalidTruckModel)]
        public string TruckModel { get; set; }

        [Required]
        [StringLength(40, MinimumLength = 2, ErrorMessage = Constants.Errors.InvalidColor)]
        public string Color { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = Constants.Errors.InvalidTruckName)]
        public string Name { get; set; }

        [Required]
        [Range(1950, 2100, ErrorMessage = Constants.Errors.InvalidTruckYear)]
        public int Year { get; set; }

        public MealTypeEnum MealType { get; set; }

        public CuisineCategoryEnum CuisineCategory { get; set; }

        public PersonalInfo Driver { get; set; }

        public PersonalInfo CookInfo { get; set; }

        [Range(0, 1000)]
        public int MaxCapacityPerMeal { get; set; }

        [Required]
        public List<ContactInfo> Contacts { get; set; }
        
        public string Description { get; set; }
        
        public List<object> AdditionalInfo { get; set; }

        public Coordination Coordination { get; set; }
    }
}
