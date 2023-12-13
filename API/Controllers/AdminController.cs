using System.Diagnostics.SymbolStore;
using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Humanizer;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly ILogger<AdminController> _logger;
        private readonly StoreContext _storeContext;
        private IGenericRepository<ProductType> _productTypeRepo;
        private IGenericRepository<ProductBrand> _productBrandRepo;

        public AdminController(ILogger<AdminController> logger,
            StoreContext context,
            IGenericRepository<ProductType> productTypeRepo,
            IGenericRepository<ProductBrand> productBrandRepo
        )
        {
            _logger = logger;
            _storeContext = context;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
        }

        [HttpPost]
        [Route("ProductType")]
        public async Task ProductType([FromBody] ProductType productType)
        {
            _logger.LogInformation(Guid.NewGuid().ToString(), productType);
            await _storeContext.ProductType.AddAsync(productType);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [Route("ProductType/{id}")]
        public async Task<ActionResult<ProductType>> Get(int id)
        {
            var res = await _productTypeRepo.GetByIdAsync(id);
            _logger.LogInformation(Guid.NewGuid().ToString(), res);
            if (res == null) return NotFound(new ApiResponse(404));
            return res;
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [Route("ProductType/{id}")]
        public async Task Update([FromBody] ProductType productType)
        {
            if (await _storeContext.ProductType.FindAsync(productType.Id) is ProductType found)
                found.Name = productType.Name;
            _logger.LogInformation(Guid.NewGuid().ToString(), "Updating ProductType");
            await _storeContext.SaveChangesAsync();
        }

        [HttpPost]
        [Route("ProductBrand")]
        public async Task ProductBrand([FromBody] ProductBrand productBrand)
        {
            _logger.LogInformation(Guid.NewGuid().ToString(), productBrand);
            await _storeContext.ProductBrand.AddAsync(productBrand);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [Route("ProductBrand/{id}")]
        public async Task<ActionResult<ProductBrand>> GetBrand(int id)
        {
            var res = await _productBrandRepo.GetByIdAsync(id);
            _logger.LogInformation(Guid.NewGuid().ToString(), res);
            if (res == null) return NotFound(new ApiResponse(404));
            return res;
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [Route("ProductBrand/{id}")]
        public async Task UpdateProductBrand([FromBody] ProductBrand productBrand)
        {
            if (await _storeContext.ProductBrand.FindAsync(productBrand.Id) is ProductBrand found)
                found.Name = productBrand.Name;
            _logger.LogInformation(Guid.NewGuid().ToString(), "Updating ProductBrand");
            await _storeContext.SaveChangesAsync();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [Route("Shop/Create")]
        public async Task<OkObjectResult> CreateShop([FromBody] Shop shop)
        {
            if (shop.Id > 0)
            {
                var s = _storeContext.Shops.Where(p => p.Id == shop.Id).First();
                if (s != null)
                {
                    s.Name = shop.Name;
                    s.Description = shop.Description;
                    s.IsActive = shop.IsActive;
                    s.IsBanned = shop.IsBanned;
                }
            }
            else
            {
                await _storeContext.Shops.AddAsync(shop);
            }
            await _storeContext.SaveChangesAsync();
            return Ok(new ApiResponse(201, "Operation was sucessfully completed"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [Route("Vendor/Create")]
        public async Task<OkObjectResult> CreateVedor([FromBody] Vendor vendor)
        {
            if (vendor.Id > 0)
            {
                var v = _storeContext.Vendors.Where(p => p.Id == vendor.Id).First();
                if (v != null)
                {
                    v.Name = vendor.Name;
                    v.Description = vendor.Description;
                    v.IsActive = vendor.IsActive;
                    v.IsOwner = vendor.IsOwner;
                    v.ShopId = vendor.ShopId;
                }
            }
            else
            {
                await _storeContext.Vendors.AddAsync(vendor);
            }
            await _storeContext.SaveChangesAsync();
            return Ok(new ApiResponse(201, "Operation was sucessfully completed"));
        }
    }
}