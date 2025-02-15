
// using Microsoft.EntityFrameworkCore;
// using RecruitmentManagement.Models;
// using RecruitmentManagement.Repositories;

// namespace RecruitmentManagement.Services;

// public class UserTypeService : IUserTypeRepository
// {
//     private readonly ApplicationContext userTypeContext;


//     public UserTypeService(ApplicationContext context){
//         userTypeContext = context;
//     }


//     //Create
//     public async Task<UserType> AddUserType(UserType userType)
//     {
//         var result =  await userTypeContext.UserTypes.AddAsync(userType);
//         await userTypeContext.SaveChangesAsync();
//         return result.Entity;
//     }


//     //Read
//     public async Task<IEnumerable<UserType>> GetUserTypes(){
//         return await userTypeContext.UserTypes.ToListAsync();
//     }

//     public async Task<UserType> GetUserTypeById(long id)
//     {
//         return await userTypeContext.UserTypes.FindAsync(id);
//     }

//     public async Task<UserType> GetUserTypeByTypeOfUser(string type)
//     {
//         return await userTypeContext.UserTypes
//                     .FirstOrDefaultAsync(ut=>ut.TypeOfUser.ToLower().Equals(type.ToLower()));
        
//     }

//     //Update
//     public async Task<UserType> UpdateUserType(long id,UserType userType)
//     {
//        var result = await userTypeContext.UserTypes
//                     .FirstOrDefaultAsync(ut=>ut.Id == id);
//         if(result != null){
//             result.TypeOfUser = userType.TypeOfUser;
//             await userTypeContext.SaveChangesAsync();

//             return result;
//         }

//         return null;
        
//     }

//     public async Task<UserType> DeleteUserType(long id)
//     {
//         var result = await userTypeContext.UserTypes
//                     .FirstOrDefaultAsync(ut=>ut.Id == id);
//         if(result != null){  
//             userTypeContext.Remove(result);
//             await userTypeContext.SaveChangesAsync();
//             return result;
//         }

//         return null;

//     }

// }