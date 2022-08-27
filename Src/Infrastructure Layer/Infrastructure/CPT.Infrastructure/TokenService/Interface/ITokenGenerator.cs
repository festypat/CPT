using CPT.Helper.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CPT.Infrastructure.TokenService.Interface
{
    public interface ITokenGenerator
    {
        Task<TokenGeneratorViewModel> GenerateToken(TokenUserDetailsViewModel tokenUserDetails);
    }
}
