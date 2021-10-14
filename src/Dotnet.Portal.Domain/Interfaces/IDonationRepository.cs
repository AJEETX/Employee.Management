using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dotnet.Portal.Domain.Models;

namespace Dotnet.Portal.Domain.Interfaces
{
    public interface IDonationRepository : IRepository<Payment>
    {
        Task<Payment> GetDonation(Guid id);
        List<Payment> GetDonations();
    }
}
