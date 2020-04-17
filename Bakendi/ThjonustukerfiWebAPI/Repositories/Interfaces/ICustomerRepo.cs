using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Repositories.Interfaces
{
    /// <summary>Repository for accessing the Database for Customers.</summary>
    public interface ICustomerRepo
    {
        /// <summary>Adds a new customer to the database.</summary>
        /// <returns>A CustomerDTO object.</returns>
        CustomerDTO CreateCustomer(CustomerInputModel customer);

        /// <summary>Gets a customer with the given ID from the database.</summary>
        /// <returns>A detailed DTO of the customer.</returns>
        CustomerDetailsDTO GetCustomerById(long id);

        /// <summary>Updates a customer in the database.</summary>
        void UpdateCustomerDetails(CustomerInputModel customer, long id);

        /// <summary>Removes a customer from the database.</summary>
        void DeleteCustomerById(long id);

        /// <summary>Checks if a customer exists in the database.</summary>
        /// <returns>True if the customer exists in the database, else false.</returns>
        bool CustomerExists(long id);

        /// <summary>Gets a list of all customers from the database.</summary>
        /// <returns>A list of all customers.</returns>
        IEnumerable<CustomerDTO> GetAllCustomers();
    }
}