using RecruitmentManagement.DTOs.JobCandidates;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Repositories;

public interface    IScheduleInterviewRepository
{
     //Create
    Task<ScheduledInterview> AddScheduledInterview(JobCandidate jobCandidate,NewScheduledInterviewDto scheduledInterviewDto,string organisationId);
    Task<ScheduledInterview> UpdateScheduledInterview(int scheduledInterviewId,UpdateScheduledInterviewDto scheduledInterviewDto);
    Task<ScheduledInterview> GetScheduledInterviewById(int id);
    Task<InterviewType> GetInterviewTypeById(int interviewTypeId);
    Task<List<string>> GetRounhandlerList(List<string> roundHandlers);
    Task<ScheduledInterview> GetUnclearedInterviewByJobCandidateId(int jobCandidateId);


    //Read
    // Task<IEnumerable<Position>> GetPositions();

    // Task<Position> GetPositionByName(string positionName);

    // //Update
    // Task<Position> UpdatePositionById(int id, Position position);

    // //Delete
    // Task<Position> DeletePositionById(int id);
}