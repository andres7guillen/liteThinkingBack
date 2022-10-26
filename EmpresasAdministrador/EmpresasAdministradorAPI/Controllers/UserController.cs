using CompanyManagerAPI.Models;
using CompanyManagerAPI.Models.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CompanyManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        [HttpPost("Registrar")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationResponseModel>> Register([FromBody] UserCredentialsModel userCredentials)
        {
            var usuario = new IdentityUser
            {
                UserName = userCredentials.Email,
                Email = userCredentials.Email
            };
            var userCreated = await _userManager.CreateAsync(usuario, userCredentials.Password);
            if (userCreated.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(userCredentials.Email);
                var roles = await _userManager.GetRolesAsync(user);
                return BuildToken(userCredentials: userCredentials, roles);                
            }
            else
            {
                string messageError = String.Join(",", userCreated.Errors.Select(e => e.Description));
                return BadRequest(messageError);
            }

        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationResponseModel>> Login([FromBody] UserCredentialsModel userCredentials)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(userCredentials.Email, userCredentials.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(userCredentials.Email);
                    var roles = await _userManager.GetRolesAsync(user);
                    return BuildToken(userCredentials: userCredentials, roles);
                }
                else
                {                    
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost("AsigneRoleToUser")]
        public async Task<IActionResult> AsignarRolUsuario(EditRoleUser model)
        {
            try
            {
                var roles = _roleManager.Roles.ToList();
                var usuario = await _userManager.FindByEmailAsync(model.Email);
                if (usuario == null) { return NotFound(); }
                
                await _userManager.AddClaimAsync(usuario, new Claim(ClaimTypes.Role,model.Role));
                var rolAsociado = await _userManager.AddToRoleAsync(usuario,model.Role);
                string Mensaje = $"Role = {model.Role} assigned correctlly to the user: {usuario.UserName}";
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw e;
            }

        }

        [HttpPost("RemoveRoleUser")]
        public async Task<IActionResult> RemoverRolUsuario(EditRoleUser model)
        {
            var usuario = await _userManager.FindByEmailAsync(model.Email);
            if (usuario == null) { return NotFound(); }
            await _userManager.RemoveClaimAsync(usuario, new Claim(ClaimTypes.Role, model.Role));
            await _userManager.RemoveFromRoleAsync(usuario, model.Role);
            string Mensaje = $"Role = {model.Role} was removed from user: {usuario.UserName}";
            return Ok(Mensaje);
        }

        private AuthenticationResponseModel BuildToken(UserCredentialsModel userCredentials, IList<string> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", userCredentials.Email)
            };

            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

           
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expirationTime = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expirationTime, signingCredentials: creds);
            return new AuthenticationResponseModel()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expirationTime
            };
        }



    }
}
