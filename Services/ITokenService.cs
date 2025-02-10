using RecruitmentManagement.Models;

namespace RecruitmentManagement.Services;

public interface ITokenService
{
    string CreateToken(Users user);
}