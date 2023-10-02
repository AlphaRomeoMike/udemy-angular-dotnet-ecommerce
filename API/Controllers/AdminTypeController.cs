using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("Admin")]
    public class AdminTypeController : BaseApiController
    {
        private readonly ILogger<AdminTypeController> _logger;
        private readonly StoreContext _storeContext;
        private IGenericRepository<ProductType> _productTypeRepo;

        public AdminTypeController(ILogger<AdminTypeController> logger,
            StoreContext context,
            IGenericRepository<ProductType> productTypeRepo
        )
        {
            _logger = logger;
            _storeContext = context;
            _productTypeRepo = productTypeRepo;
        }

        [HttpPost]
        [Route("ProductType")]
        public async Task ProductType([FromBody] ProductType productType)
        {
            await _storeContext.ProductType.AddAsync(productType);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [Route("ProductType/{id}")]
        public async Task<ActionResult<ProductType>> Get(int id)
        {
            var res = await _productTypeRepo.GetByIdAsync(id);
            if (res == null) return NotFound(new ApiResponse(404));
            return res;
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [Route("ProductType/{id}")]
        public async Task Update([FromRoute] int id, [FromBody] ProductType productType)
        {
            if (await _storeContext.ProductType.FindAsync(id) is ProductType found)
                found.Name = productType.Name;
            await _storeContext.SaveChangesAsync();
        }
    }
}