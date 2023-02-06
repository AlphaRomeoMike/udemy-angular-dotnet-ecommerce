using System.Security.Claims;
using API.DTOs;
using API.Errors;
using API.Extenstions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ILogger<AccountController> logger,
        ITokenService tokenService,
        IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            _logger.LogInformation(Guid.NewGuid().ToString(), user);
            var res = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!res.Succeeded) return Unauthorized(new ApiResponse(401));
            return new UserDto
            {
                Email = user.Email,
                Username = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto info)
        {
            if (CheckEmailExistsAsync(info.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationResponse { Errors = new[] { "Email Address already in use" } });
            }
            var user = new AppUser
            {
                DisplayName = info.DisplayName,
                Email = info.Email,
                UserName = info.Email
            };

            var res = await _userManager.CreateAsync(user, info.Password);
            if (!res.Succeeded) return BadRequest(new ApiResponse(400));
            return new UserDto
            {
                Email = user.Email,
                Username = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrincipal(User);
            return new UserDto
            {
                Email = user.Email,
                Username = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddress(User);
            return _mapper.Map<Address, AddressDto>(user.Address);
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddress(HttpContext.User);
            user.Address = _mapper.Map<AddressDto, Address>(address);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
    }
}