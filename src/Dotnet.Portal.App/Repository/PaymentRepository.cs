using Dotnet.Portal.Domain.Models;
using Dotnet.Portal.Domain.Interfaces;
using Dotnet.Portal.Data.Context;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Portal.Data.Repository
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(DotnetPortalDB context) : base(context) { }

        public async Task<Payment> GetDonation(Guid id)
        {
            return await Db.Donations.AsNoTracking()
                .Include(d => d.Member)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public List<Payment> GetDonations()
        {
            return Db.Donations.Include(d=>d.Member).ToList();
        }
    }
}
