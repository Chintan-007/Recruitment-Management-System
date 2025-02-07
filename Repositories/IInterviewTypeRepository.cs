
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Repositories;

public interface IInterviewTypeRepository
{
    //Create
    Task<InterviewType> AddInterviewType(InterviewType interviewType);

    //Read
    Task<IEnumerable<InterviewType>> GetInterviewTypes();
    Task<InterviewType> GetInterviewTypeById(int id);
    Task<InterviewType> GetInterviewTypeByType(string type);

    //Update
    Task<InterviewType> UpdateInterviewTypeById(int id, InterviewType interviewType);

    //Delete
    Task<InterviewType> DeleteInterviewTypeById(int id);
}