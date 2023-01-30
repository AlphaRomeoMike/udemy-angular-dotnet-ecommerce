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
using Core.Specifications;
using API.DTOs;
using AutoMapper;
using API.Errors;
using API.Helpers;

namespace API.Controllers
{
   public class ProductController : BaseApiController
   {
      private readonly ILogger<ProductController> _logger;
      private readonly IGenericRepository<Product> _productRepo;
      private readonly IGenericRepository<ProductBrand> _productBrandRepo;
      private readonly IGenericRepository<ProductType> _productTypeRepo;
      private readonly IMapper _mapper;

      public ProductController(
         ILogger<ProductController> logger,
         IGenericRepository<Product> productRepo,
         IGenericRepository<ProductBrand> productBrandRepo,
         IGenericRepository<ProductType> prouctTypeRepo,
         IMapper mapper
         )
      {
         _logger = logger;
         _productRepo = productRepo;
         _productBrandRepo = productBrandRepo;
         _productTypeRepo = prouctTypeRepo;
         _mapper = mapper;
      }

      [HttpGet]
      public async Task<ActionResult<Pagination<ProductToReturnDto>>> Products([FromQuery] ProductSpecPrams productSpec)
      {
         _logger.LogInformation("Listing all data");
         var spec = new ProductWithTypeAndBrandSpecification(productSpec);
         var countSpec = new ProductWithFiltersForCountSpecification(productSpec);
         var totalItems = await _productRepo.CountAsync(countSpec);
         var products = await _productRepo.ListAsync(spec);
         var data = _mapper.Map<List<Product>, List<ProductToReturnDto>>(products);
         return new Pagination<ProductToReturnDto>(productSpec.PageIndex, productSpec.PageSize, totalItems, data);
      }

      [HttpGet("{id}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
      public async Task<ActionResult<ProductToReturnDto>> Product(int? id)
      {
         _logger.LogInformation($"Getting info for {id}");
         var spec = new ProductWithTypeAndBrandSpecification(id);
         var product = await _productRepo.GetEntityWithSpec(spec);
         if (product == null) return NotFound(new ApiResponse(404));

         return _mapper.Map<Product, ProductToReturnDto>(product);
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