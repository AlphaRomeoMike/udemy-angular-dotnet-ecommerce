using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class AdminService
    {
        private IGenericRepository<Vendor> _vendorRepo;

        private ILogger<AdminService> _logger;
        public void CreateVendorAsync(Vendor vendor)
        {
            _logger.LogDebug("Vendor Information", vendor);
            _vendorRepo.Add(vendor);
        }
    }
}