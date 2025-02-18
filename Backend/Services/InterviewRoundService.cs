using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagement.DTOs;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Service;


public class InterviewRoundService : IInterviewRoundRepository
{
    private readonly ApplicationContext applicationContext;

    public InterviewRoundService(ApplicationContext applicationContext){
        this.applicationContext = applicationContext;
    }


    public async Task<RoundHandler> GetInterviewRoundByEmployeeIdAndInterviewId(int scheduledInterviewId, string employeeId)
    {
        var result = await applicationContext.RoundHandlers
                                     .Include(rh=>rh.scheduledInterview)
                                    .FirstOrDefaultAsync(rh=> rh.scheduledInterviewId == scheduledInterviewId && rh.employeeId == employeeId);
        return result;
    }
    public async Task<RoundHandler> AddFeedBack(AddFeedBackDto addFeedBackDto, RoundHandler interviewRound)
    {
        interviewRound.rating = addFeedBackDto.rating;
        interviewRound.feedback = addFeedBackDto.feedback;
        interviewRound.isCompleted = addFeedBackDto.isCompleted;

        await applicationContext.SaveChangesAsync();

        return interviewRound;
    }

    public async Task<Employee> GetInterviewerById(string employeeId)
    {
        return await applicationContext.Employees.FindAsync(employeeId);
    }

    public async Task<Candidate> GetCandidateById(string candidateId)
    {
        return await applicationContext.Candidates.FindAsync(candidateId);
    }

    public async Task<IEnumerable<RoundHandler>> GetInterviewRoundByEmployeeId(string employeeId)
    {
        var result = await applicationContext.RoundHandlers
                            .Include(rh=>rh.employee)
                            .Include(rh=>rh.scheduledInterview)
                            .Where(rh=>String.Equals(rh.employeeId,employeeId)).ToListAsync();
        return result;
    }
}