using RecruitmentManagement.DTOs;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Mappers;

public static class InterviewRoundMapper
{
    public static GetInterviewRoundDto ModelToGetInterviewRoundDto(this RoundHandler roundHandler,string candidate,string interviewType,string interviewer){
        return new GetInterviewRoundDto{
            candidate = candidate,
            interviewType = interviewType,
            interviewer = interviewer,
            rating = roundHandler.rating,
            feedback = roundHandler.feedback
        };

    }
}