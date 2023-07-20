using BookRentalSystem.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookRentalSystem.Models.ErrorHandling;
using BookRentalSystem.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookRentalSystem.Controllers
{
    //[EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<Customer> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<Customer> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] SignupModel model)
        {
            try
            {
                //check if already exists
                var userExists = await _userManager.FindByEmailAsync(model.Email!);
                if (userExists != null)
                {
                    return StatusCode(StatusCodes.Status403Forbidden,
                        new Response { Status = "Error", Message = "User already exists with that Email.", Reason = "-" });
                }

                //add identity user
                Customer user = new()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };

                //create user in db
                var result = await _userManager.CreateAsync(user, model.Password!);

                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                            new Response
                            {
                                Status = "Error",
                                Message = $"User failed to create.",
                                Reason = $"{result}"
                            });
                }

                await _userManager.AddToRoleAsync(user, "Customer");
                return StatusCode(StatusCodes.Status201Created,
                    new Response { Status = "Success", Message = "User Created Successfully", Reason = "-" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "Exception thrown.", Reason = $"{ex.Message}" });
            }
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email!);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new Response { Status = "Error", Message = "No user is registered against this email." });
            }
            else if(await _userManager.CheckPasswordAsync(user, model.Password!))
            {
                //claimlist creation
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.NameIdentifier, user.Id ),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                //add roles to claimlist
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach(var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                //generate token with claims
                var jwtToken = GenerateToken(authClaims);
                //return token
                return Ok(new
                {
                    Username = user.UserName,
                    token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    expiration = jwtToken.ValidTo
                });
            }
            return Unauthorized();
        }

        private JwtSecurityToken GenerateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
