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
    public class DonationRepository : Repository<Donation>, IDonationRepository
    {
        public DonationRepository(DotnetPortalDB context) : base(context) { }

        public async Task<Donation> GetDonation(Guid id)
        {
            return await Db.Donations.AsNoTracking()
                .Include("Donation.Member")
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public List<Donation> GetDonations()
        {
            return Db.Donations.ToList();
        }
    }
}
