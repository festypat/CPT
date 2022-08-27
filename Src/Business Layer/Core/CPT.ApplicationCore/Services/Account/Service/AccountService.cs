using AutoMapper;
using CPT.ApplicationCore.Services.Account.Interface;
using CPT.Domain.Entities;
using CPT.Helper.Dto.Request.Account;
using CPT.Helper.Dto.Response;
using CPT.Helper.Dto.Response.Account;
using CPT.Helper.ViewModel;
using CPT.Infrastructure.TokenService.Interface;
using CPT.Persistence.Repositories.Customer;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace CPT.ApplicationCore.Services.Account.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IMapper _mapper;
        private readonly CustomerProfileService _customerProfileService;
        public AccountService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, ITokenGenerator tokenGenerator,
            IMapper mapper, CustomerProfileService customerProfileService)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
            _customerProfileService = customerProfileService ?? throw new ArgumentNullException(nameof(customerProfileService));

            ////var config = new MapperConfiguration(cfg => cfg.CreateMap<RegisterUserRequestDto, ApplicationUser>());

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CreateUserBaseResponseDto> RegisterUserAsync(RegisterUserRequestDto model)
        {
            var response = new AppResponse();
            try
            {
                var user = _mapper.Map<RegisterUserRequestDto, ApplicationUser>(model);

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    response.Data = result.Errors;

                    return new CreateUserBaseResponseDto
                    {
                        StatusCode = HttpResponseCodes.BadRequest,
                        Message = "Duplicate user details. Please try again",
                        Success = false,
                        ResponseCode = "01",
                        //data = JsonConvert result.Errors
                    };
                }

                await _userManager.AddToRoleAsync(user, "Administrator");

                return new CreateUserBaseResponseDto
                {
                    StatusCode = HttpResponseCodes.Success,
                    Message = "Success",
                    Success = true,
                    ResponseCode = "00"
                };
            }
            catch (Exception)
            {

                return new CreateUserBaseResponseDto
                {
                    StatusCode = HttpResponseCodes.InternalError,
                    Message = "An error occured while creating account. Please try again",
                    Success = false,
                    ResponseCode = "02"
                };
            }

        }
        public async Task<LoginBaseResponseDto> AuthenticateUser(LoginRequestDto login)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(login.Username);

                if (user != null)
                {
                    var signInResult = await _userManager.CheckPasswordAsync(user, login.Password);

                    if (signInResult)
                    {
                        var roles = await _userManager.GetRolesAsync(user);

                        var userModel = new TokenUserDetailsViewModel
                        {
                            Email = user.Email,
                            Username = user.UserName,
                            Role = roles[0]
                        };

                        var userInfo = await _customerProfileService.CustomerInfo(user.Id);

                        if(userInfo == default)
                            return new LoginBaseResponseDto
                            {

                                StatusCode = HttpResponseCodes.BadRequest,
                                Message = "Login was not successful. Please try again",
                                Success = false,
                                ResponseCode = "01",
                            };

                        userModel.CustomerId = userInfo.CustomerId;

                        var token = await _tokenGenerator.GenerateToken(userModel);

                        if (token.ResponseCode == "00")
                            return new LoginBaseResponseDto
                            {
                                CustomerId = userInfo.CustomerId,
                                StatusCode = HttpResponseCodes.Success,
                                Message = "Success",
                                Success = true,
                                ResponseCode = "00",
                                data = _mapper.Map<TokenGeneratorViewModel, LoginResponseDto>(token)
                            };

                        return new LoginBaseResponseDto
                        {

                            StatusCode = HttpResponseCodes.BadRequest,
                            Message = "Login was not successful. Please try again",
                            Success = false,
                            ResponseCode = "01",
                        };

                    }

                    return new LoginBaseResponseDto
                    {
                        StatusCode = HttpResponseCodes.BadRequest,
                        Message = "Invalid login. Please try again",
                        Success = false,
                        ResponseCode = "02"
                    };
                }

                return new LoginBaseResponseDto
                {
                    StatusCode = HttpResponseCodes.BadRequest,
                    Message = "Invalid login details. Please try again",
                    Success = false,
                    ResponseCode = "02"
                };
            }
            catch (Exception ex)
            {
                return new LoginBaseResponseDto
                {
                    StatusCode = HttpResponseCodes.InternalError,
                    Message = "An error occured while creating account. Please try again",
                    Success = false,
                    ResponseCode = "02"
                };
            }
        }
    }
}
