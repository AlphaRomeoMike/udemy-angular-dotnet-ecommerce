using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Contexts;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Core.Interfaces;

namespace API.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class ProductController : ControllerBase
   {
      private readonly ILogger<ProductController> _logger;
      private readonly IGenericRepository<Product> _productRepo;
      private readonly IGenericRepository<ProductBrand> _productBrandRepo;
      private readonly IGenericRepository<ProductType> _productTypeRepo;

      public ProductController(ILogger<ProductController> logger, 
      IGenericRepository<Product> productRepo, 
      IGenericRepository<ProductBrand> productBrandRepo, 
      IGenericRepository<ProductType> prouctTypeRepo)
      {
         _logger = logger;
         _productRepo = productRepo;
         _productBrandRepo = productBrandRepo;
         _productTypeRepo = prouctTypeRepo;
      }

      [HttpGet]
      public async Task<ActionResult<List<Product>>> Products()
      {
         _logger.LogInformation("Listing all data");
         return await _productRepo.ListAllAsync();
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<Product?>> Product(int? id)
      {
        _logger.LogInformation($"Getting info for {id}");
        return await _productRepo.GetByIdAsync(id);
      }

      [HttpGet("brands")]
      public async Task<ActionResult<List<ProductBrand>>> GetProductBrand()
      {
         return await _productBrandRepo.ListAllAsync();
      }

      [HttpGet("types")]
      public async Task<ActionResult<List<ProductType>>> GetProductTypes()
      {
         return await _productTypeRepo.ListAllAsync();
      }
   }
}