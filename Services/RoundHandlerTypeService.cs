using Microsoft.EntityFrameworkCore;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Services;

public class InterviewTypeService : IInterviewTypeRepository
{
    private readonly ApplicationContext applicationContext;
    public InterviewTypeService(ApplicationContext applicationContext){
        this.applicationContext = applicationContext;
    }

    //Create
    public async Task<InterviewType> AddInterviewType(InterviewType interviewType)
    {
        var result =  await applicationContext.InterviewTypes.AddAsync(interviewType);
        await applicationContext.SaveChangesAsync();
        return result.Entity;

    }


    //Read

    public async Task<IEnumerable<InterviewType>> GetInterviewTypes()
    {
        return await applicationContext.InterviewTypes.ToListAsync();
    }
    public async Task<InterviewType> GetInterviewTypeById(int id)
    {
        return await applicationContext.InterviewTypes.FindAsync(id);
    }
    public async Task<InterviewType> GetInterviewTypeByType(string type)
    {
        return await applicationContext.InterviewTypes.FirstOrDefaultAsync(rhType => rhType.interviewType.Equals(type));
    }



    //Update
    public async Task<InterviewType> UpdateInterviewTypeById(int id, InterviewType interviewType)
    {
        var result = await GetInterviewTypeById(id);
        if(result != null){
            result.interviewType = interviewType.interviewType;
            await applicationContext.SaveChangesAsync();
            return result;
        }
        return null;
    }


    //Delete
    public async Task<InterviewType> DeleteInterviewTypeById(int id)
    {
        var result = await GetInterviewTypeById(id);
        if(result != null){
            applicationContext.Remove(result);
            await applicationContext.SaveChangesAsync();
            return result;
        }
        return null;
    }
}