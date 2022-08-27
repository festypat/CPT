using CPT.Helper.Dto.Request;
using CPT.Helper.Dto.Request.Account;
using CPT.Helper.Dto.Response.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace CPT.ApplicationCore.Services.Account.Interface
{
    public interface IAccountService
    {       
        Task<CreateUserBaseResponseDto> RegisterUserAsync(RegisterUserRequestDto model);
        Task<LoginBaseResponseDto> AuthenticateUser(LoginRequestDto login);
    }
}
