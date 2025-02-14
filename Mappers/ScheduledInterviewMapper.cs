using Microsoft.Identity.Client;
using RecruitmentManagement.DTOs.JobCandidates;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Mappers;

public static class ScheduledInterviewMapper
{
    public static AddedScheduledInterviewDto ModelToAddedScheduldeInterviewDto(this ScheduledInterview scheduledInterviewModel,string interviewType,List<string> roundHandlers){
        return new AddedScheduledInterviewDto{
            jobOpeningId = scheduledInterviewModel.jobOpeningId,
            candidateId = scheduledInterviewModel.candidateId,
            interviewDate = scheduledInterviewModel.interviewDate,
            interviewType = interviewType,
            roundHandlers = roundHandlers
        };
    }

    public static AfterUpdateScheduleInterviewDto ModelToUpdatedScheduldeInterviewDto(this ScheduledInterview scheduledInterviewModel,string interviewType,List<string> roundHandlers){
        return new AfterUpdateScheduleInterviewDto{
            jobOpeningId = scheduledInterviewModel.jobOpeningId,
            candidateId = scheduledInterviewModel.candidateId,
            interviewDate = scheduledInterviewModel.interviewDate,
            rating = scheduledInterviewModel.rating,
            feedback = scheduledInterviewModel.feedback,
            interviewType = interviewType,
            roundHandlers = roundHandlers,
        };
    }
}