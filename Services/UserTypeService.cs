
using Microsoft.EntityFrameworkCore;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Services;

public class UserTypeService : IUserTypeRepository
{
    private readonly UserTypeContext userTypeContext;

    public UserTypeService(UserTypeContext context){
        userTypeContext = context;
    }

    public async Task<IEnumerable<UserType>> GetUserTypes(){
        return await userTypeContext.UserTypes.ToListAsync();
    }

    public async Task<UserType> GetUserTypeById(long id)
    {
        return await userTypeContext.UserTypes.FindAsync(id);
    }

    public async Task<UserType> AddUserType(UserType userType)
    {
        var result =  await userTypeContext.UserTypes.AddAsync(userType);
        await userTypeContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<UserType> UpdateUserType(UserType userType)
    {
       var result = await userTypeContext.UserTypes
                    .FirstOrDefaultAsync(ut=>ut.Id == userType.Id);
        if(result != null){
            result.TypeOfUser = userType.TypeOfUser;
            await userTypeContext.SaveChangesAsync();

            return result;
        }

        //ToDo: Send Message: No user found
        return null;
        
    }

    public async void DeleteUserType(long id)
    {
        var result = await userTypeContext.UserTypes
                    .FirstOrDefaultAsync(ut=>ut.Id == id);
        if(result != null){
            
            userTypeContext.Remove(result);
            await userTypeContext.SaveChangesAsync();

        //ToDo: Send Message: User Deleted Sucessfully
        }

        //ToDo: Send Message: No user found

    }
}