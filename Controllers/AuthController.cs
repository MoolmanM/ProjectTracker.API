using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using TaskManager.Dtos.Auth;
using TaskManager.Models;
using TaskManager.Services;
using TaskManager.Services.Auth;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService) : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly ITokenService _tokenService = tokenService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (registerDto.Password != registerDto.ConfirmPassword)
                return BadRequest("Passwords do not match");

            var user = new ApplicationUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new AuthResponseDto
            {
                AccessToken = _tokenService.GenerateAccessToken(user),
                RefreshToken = _tokenService.GenerateRefreshToken(),
                ExpiresAt = DateTime.UtcNow.AddMinutes(60), // Should come from the config
                UserId = user.Id,
                Email = user.Email!
            });
        }

        // TODO: Add Login endpoint
    }
}