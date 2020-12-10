using System.Data;
using TestMVC_HoteLandLyst.Models;

namespace TestMVC_HoteLandLyst.DalClasses
{
    public interface ICustomerAccess
    {
        void CreateCustomer(Customer customer);
        bool FindCustomer(string phoneNumber);
        DataRow GetCustomer(string phoneNumber);
    }
}