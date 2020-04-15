using System.Collections;
using System.Collections.Generic;
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
        /// <returns>An empty list if person is deleted, else list of active orders for that customer.</returns>
        List<OrderDTO> DeleteCustomerById(long id);

        /// <summary>Gets all customers.</summary>
        /// <returns>A list of all customers.</returns>
        IEnumerable GetAllCustomers();

        /// <summary>Deletes customer and all of their active orders</summary>
        void DeleteCustomerByIdAndOrders(long customerId);
    }
}