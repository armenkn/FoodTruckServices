using FoodTruckServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckServices.DataAccessLayer
{
    public interface IContactSqlAccess
    {
        int CreateContact(ContactInfo contact, int userId);

        ContactInfo GetContactById(int contactId);

        void UpdateContact(ContactInfo contact, int userId);
    }
}
