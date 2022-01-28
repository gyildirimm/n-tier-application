using Core.Entities.SpecialAuth;

namespace Core.Utilities.Security.SpecialAuth.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }

}
