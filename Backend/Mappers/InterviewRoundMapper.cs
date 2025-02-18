using RecruitmentManagement.DTOs;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Mappers;

public static class InterviewRoundMapper
{
    public static GetInterviewRoundDto ModelToGetInterviewRoundDto(this RoundHandler roundHandler,string candidate,string candidateId,string interviewType,string interviewer,string interviewerId){
        return new GetInterviewRoundDto{
            candidateId = candidateId,
            candidate = candidate,
            interviewType = interviewType,
            interviewerId = interviewerId,
            interviewer = interviewer,
            rating = roundHandler.rating,
            feedback = roundHandler.feedback,
            isCompleted = roundHandler.isCompleted
        };

    }
}