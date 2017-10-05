
namespace FoodTruckServices.Model
{
    public static class Constants
    {
        public static string GoogleMapApiKey = "AIzaSyAfQ3Ga_MtLW7pc6ZejXVYhSpawbD9s4UY";

        public static string GoogleMapApiUrl = "https://maps.googleapis.com/maps/api/geocode/json?address={0}&key=" + GoogleMapApiKey;

        public static class Tokens
        {
            public const int AccessTokenExpirationMinutes = 4120;
            public const int RefreshTokenExpirationMinutes = 7200; //5days
            public const string Algorithm = "HS256";
            public const string Type = "JWT";
            public const string UserInfo = "userinfo";
            

            public static class Providers
            {
                public const string AdminTokenProvider = "AdminTokenProvider";
            }
        }

        public static class Cache
        {

        }

        public static class Errors
        {
            public const string InvaidFirstName = "First name is invalid";
            public const string InvaidLastName = "Last name is invalid";
            public const string InvaidMiddleName = "Middle name is invalid";
            public const string InvalidContactText = "Contact info is invalid";
            public const string InvalidLicensePlate = "License plate is invalid";
            public const string InvalidTruckMake = "Truck make is invalid";
            public const string InvalidTruckModel = "Truck model is invalid";
            public const string InvalidColor = "Color is invalid";
            public const string InvalidTruckName = "Truck name is invalid";
            public const string InvalidTruckYear = "Truck year is invalid";
            public const string InvalidHealthCode = "Health code is invalid";
            public const string InvalidBusinessName = "Business name is invalid";
            public const string InvalidPermitNumber = "Permit number is invalid";
            public const string InvalidAddress1 = "Address1 is invalid";
            public const string InvalidAddress2 = "Address2 is invalid";
            public const string InvalidCity = "City is invalid";
            public const string InvalidZipcode = "Zipcode is invalid";
            public const string InvalidState = "State is invalid";

        }
    }

}
