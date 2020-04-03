using System.Collections;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        /// <summary>Creates a new customer.</summary>
        /// <returns>A customer DTO of the customer that was just created.</returns>
        CustomerDTO CreateCustomer(CustomerInputModel customer);

        /// <summary>Gets a customer with the given ID.</summary>
        /// <returns>A detailed customer DTO.</returns>
        CustomerDetailsDTO GetCustomerById(long id);

        /// <summary>Updates a customer with the given ID and input.</summary>
        void UpdateCustomerDetails(CustomerInputModel customer, long id);

        /// <summary>Deletes a customer with the given ID.</summary>
        void DeleteCustomerById(long id);

        /// <summary>Gets all customers.</summary>
        /// <returns>A list of all customers.</returns>
        IEnumerable GetAllCustomers();
    }
}