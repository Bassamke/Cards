using AutoMapper;
using Cards.DTOs;
using Cards.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IAuthService authService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _authService = authService;
        }
        [HttpPost("SignUp")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegistrationDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = _mapper.Map<IdentityUser>(model);

                    var result = await _userManager.CreateAsync(newUser, model.Password);

                    if (result.Succeeded)
                    {

                        IdentityUserRole<string> userRole = new IdentityUserRole<string>
                        {
                            UserId = newUser.Id,
                            RoleId = model.Role
                        };
                        
                        var AddUserRoleResult = await AddUserRole(userRole);

                        return CreatedAtAction("Register", new { id = newUser.Id }, newUser);
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }
                return BadRequest(ModelState);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            

        }
        [HttpPost]
        [Route("AdduserRole")]
        public async Task<IActionResult> AddUserRole(IdentityUserRole<string> userRole)
        {
            var role = await _roleManager.FindByIdAsync(userRole.RoleId);
            if (role == null)
            {
                return BadRequest(new { error = "Role not found" });
            }
            else
            {
                var userDetails = await _userManager.FindByIdAsync(userRole.UserId);
                if (!await _userManager.IsInRoleAsync(userDetails, role.Name))
                {
                    IdentityResult result = await _userManager.AddToRoleAsync(userDetails, role.Name);
                    if (result.Succeeded)
                    {
                    }

                }
                else
                {
                }
                return Ok();
            }
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.Email);
                        var roleName = await _userManager.GetRolesAsync(appUser);
                        if (roleName.Count > 0)
                        {
                            var role = await _roleManager.FindByNameAsync(roleName[0]);
                            LoginResponse response= _authService.GenerateJwtToken(appUser, role);
                            return Ok(response);
                        }
                    }
                    ModelState.AddModelError("", "Invalid Credentials, please try again");
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
    }
}
