using CPT.Helper.ViewModel;
using CPT.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CPT.Persistence.Repositories.Customer
{
    public class CustomerProfileService
    {
        private readonly CPTDbContext _context;
        public CustomerProfileService(CPTDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<CustomerViewModel> CustomerInfo(string userId)
        {
            try
            {
                var user = new Guid(userId);

                var customer = await _context.Customers.SingleOrDefaultAsync(c => c.UserId == user);
                
                if(customer != default)
                {
                    return new CustomerViewModel
                    {
                        CustomerId = customer.CustomerId,
                        UserId = customer.UserId
                    };
                }

                return new CustomerViewModel();
              
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
