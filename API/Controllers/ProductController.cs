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

namespace API.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class ProductController : ControllerBase
   {
      private readonly ILogger<ProductController> _logger;
      private readonly StoreContext context;

      public ProductController(ILogger<ProductController> logger, 
      StoreContext context)
      {
         _logger = logger;
         this.context = context;
      }

      [HttpGet]
      public async Task<ActionResult<List<Product>>> Products()
      {
         _logger.LogInformation("Listing all data");
         var products = await context.Products.ToListAsync();
         return products;
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<Product?>> Product(int? id)
      {
        _logger.LogInformation("Getting all data");
        return await context.Products.FindAsync(id);
      }
   }
}