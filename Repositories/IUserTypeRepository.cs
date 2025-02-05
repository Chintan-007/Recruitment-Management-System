using RecruitmentManagement.Models;

namespace RecruitmentManagement.Repositories;

public interface IUserTypeRepository
{
    Task<IEnumerable<UserType>> GetUserTypes();

    Task<UserType> GetUserTypeById(long id);

    Task<UserType> AddUserType(UserType userType);

    Task<UserType> UpdateUserType(long id,UserType userType);

    Task<UserType> DeleteUserType(long id);
    Task<UserType> GetUserTypeByTypeOfUser(string type);
}