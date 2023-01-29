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
      private readonly IProductRepository _repo;

      public ProductController(ILogger<ProductController> logger, IProductRepository repo)
      {
         _logger = logger;
         _repo = repo;
      }

      [HttpGet]
      public async Task<ActionResult<List<Product>>> Products()
      {
         _logger.LogInformation("Listing all data");
         return await _repo.GetProducts();
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<Product?>> Product(int? id)
      {
        _logger.LogInformation($"Getting info for {id}");
        return await _repo.GetProductByIdAsync(id);
      }
   }
}