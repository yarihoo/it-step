using AutoMapper;
using AutoMapper.Execution;
using Internet_Market_WebApi.Abstract;
using Internet_Market_WebApi.Constants;
using Internet_Market_WebApi.Data;
using Internet_Market_WebApi.Data.Entities;
using Internet_Market_WebApi.Data.Entities.Identity;
using Internet_Market_WebApi.Data.Entities.Products;
using Internet_Market_WebApi.Models;
using Internet_Market_WebApi.Models.Google;
using Internet_Market_WebApi.Models.Saving_Images;
using Internet_Market_WebApi.Services;
using Internet_Market_WebApi.Services.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tsp;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Internet_Market_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly ISmtpEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly AppEFContext _context;
        public UserController(IJwtTokenService jwtTokenService,
            UserManager<UserEntity> userManager,
            ISmtpEmailService emailService,
            IConfiguration configuration, AppEFContext context)
        {
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
            _context = context; 
        }
        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (!isPasswordValid)
                    {
                        return BadRequest();

                    }
                    var token = _jwtTokenService.CreateToken(user);
                    return Ok(new { token });
                }   
                return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("Password unconfirmed");
            }
            UserEntity user = new UserEntity()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
            };
            try
            {
                var result = _userManager.CreateAsync(user, model.Password).Result;
                if (result.Succeeded)
                {
                    result = _userManager.AddToRoleAsync(user, Roles.User).Result;
                    //Sending mail to confirm account
                    //
                    //
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var myUrl = @"http://localhost:3000";

                    var callbackUrl = $"{myUrl}/accountConfirm?userId={user.Id}&" +
                        $"code={WebUtility.UrlEncode(token)}";

                    var message = new Message()
                    {
                        To = user.Email,
                        Subject = "Confirm Email",
                        Body = "To confirm email click reference below:" +
                            $"<a href='{callbackUrl}'>Confirm</a>"
                    };
                    _emailService.Send(message);
                    //
                    //
                    //
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();

            }

        }
        [HttpPost("/GetUser")]
        public async Task<IActionResult> GetUser(JwtTokenViewModel model)
        {
            try
            {
                string email = _jwtTokenService.GetEmailFromToken(model.Data);
                UserEntity user = await _userManager.FindByEmailAsync(email);
                return Ok(new
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                });
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("/UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserViewModel model)
        {
            try
            {
                string email = _jwtTokenService.GetEmailFromToken(model.Jwt);
                UserEntity user = await _userManager.FindByEmailAsync(email);
                user.Email = model.User.Email;
                user.FirstName = model.User.FirstName;
                user.LastName = model.User.LastName;
                await _userManager.UpdateAsync(user);
                var token = _jwtTokenService.CreateToken(user);
                return Ok(new { token });
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("/forgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound();
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var frontendUrl = _configuration.GetValue<string>("FrontEndURL");

            var myUrl = @"http://localhost:3000";

            var callbackUrl = $"{myUrl}/resetComplete?userId={user.Id}&" +
                $"code={WebUtility.UrlEncode(token)}";

            var message = new Message()
            {
                To = user.Email,
                Subject = "Reset Password",
                Body = "To reset password click reference below:" +
                    $"<a href='{callbackUrl}'>Reset Password</a>"
            };
            _emailService.Send(message);
            return Ok();
        }
        [HttpPost("/changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                var res = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("/confirmAccount")]
        public async Task<IActionResult> ConfirmAccount(ConfirmAccountModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                await _userManager.ConfirmEmailAsync(user, model.Token);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("/MakePayment")]
        public async Task<IActionResult> MakePayment(PaymentFormModel model)
        {
            try
            {
                ////DO SOME
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("/SaveUserImage")]
        public async Task<IActionResult> SaveUserImage(SaveImageViewModel model)
        {
            try
            {
                string email = _jwtTokenService.GetEmailFromToken(model.Jwt);
                UserEntity user = await _userManager.FindByEmailAsync(email);
                var list = _context.Images
                    .Where(x => x.User.Id == user.Id)
                    .Where(y => y.IsDelete == false).ToList();
                if (list.Count > 0)
                {
                    foreach (var image in list)
                    {
                        image.IsDelete = true;  
                    }
                }
                ImageEntity newImage = new ImageEntity
                {
                    ImageData = model.ImageUrl,
                    User = user,
                };
                _context.Images.Add(newImage);
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("/GetAccountImage")]
        public async Task<IActionResult> GetUserImage(JwtTokenViewModel model)
            {
            try
            {
                string email = _jwtTokenService.GetEmailFromToken(model.Data);
                UserEntity user = await _userManager.FindByEmailAsync(email);
                var myUser = await _context.Users
                    .Include(us => us.Images)
                    .SingleOrDefaultAsync(x => x.Id == user.Id);
                var image = myUser.Images.FirstOrDefault(x => x.IsDelete == false);
                if (image != null)
                    return Ok(image.ImageData);
                else
                    return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("/google/registartion")]
        public async Task<IActionResult> GoogleRegistartion(GoogleLogInViewModel model)
        {
            try
            {
            var payload = await _jwtTokenService.VerifyGoogleToken(model.Token);
            if (payload == null)
            {
                return BadRequest();
            }
            string provider = "Google";
            var info = new UserLoginInfo(provider, payload.Subject, provider);
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    string base64String = "";
                    using (WebClient client = new WebClient())
                    {
                        byte[] imageBytes = client.DownloadData(payload.Picture);
                        base64String = Convert.ToBase64String(imageBytes);
                    }
                    model.ImagePath = base64String;
                    user = new UserEntity
                    {
                        Email = payload.Email,
                        UserName = payload.Email,
                        FirstName = payload.GivenName,
                        LastName = payload.FamilyName,

                    };
                    var resultCreate = await _userManager.CreateAsync(user);
                    if (!resultCreate.Succeeded)
                    {
                        return BadRequest();
                    }

                    await _userManager.AddToRoleAsync(user, Roles.User);
                    if (model.ImagePath != null)
                    {
                        _context.Images.Add(new ImageEntity()
                        { 
                            DateCreated = DateTime.Now,
                            User = user,
                            ImageData = model.ImagePath
                        });
                        _context.SaveChanges();
                    }

                    return Ok();

                }
            }

            return BadRequest();
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPost("/google/login")]
        public async Task<IActionResult> GoogleLogin(GoogleLogInViewModel model)
        {
            var payload = await _jwtTokenService.VerifyGoogleToken(model.Token);
            var token = "";
            if (payload == null)
            {
                return BadRequest();
            }
            string provider = "Google";
            var info = new UserLoginInfo(provider, payload.Subject, provider);
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            bool isNewUser = false;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    isNewUser = true;

                    user = new UserEntity
                    {
                        Email = payload.Email,
                        UserName = payload.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                    };
                    return Ok(new { isNewUser, token });

                }

                var resultuserLogin = await _userManager.AddLoginAsync(user, info);
                if (!resultuserLogin.Succeeded)
                {
                    return BadRequest();
                }
            }

            token = await _jwtTokenService.CreateToken(user);

            return Ok(new { isNewUser, user, token });
        }

    }
}
        
