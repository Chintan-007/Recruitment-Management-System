using RecruitmentManagement.Models;

namespace RecruitmentManagement.Services;

public interface ITokenService
{
    Task<string> CreateToken(Users user);
}