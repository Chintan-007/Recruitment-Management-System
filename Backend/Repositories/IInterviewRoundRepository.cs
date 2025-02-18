using Microsoft.AspNetCore.Mvc;
using RecruitmentManagement.DTOs;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Repositories;

public interface IInterviewRoundRepository
{
    public  Task<RoundHandler> GetInterviewRoundByEmployeeIdAndInterviewId(int scheduledInterviewId, string employeeId);
    public Task<RoundHandler> AddFeedBack(AddFeedBackDto addFeedBackDto,RoundHandler interviewRound);

    public Task<Employee> GetInterviewerById(string employeeId);
    public Task<Candidate> GetCandidateById(string candidateId);
    Task<IEnumerable<RoundHandler>> GetInterviewRoundByEmployeeId(string employeeId);
}