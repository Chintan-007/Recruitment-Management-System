using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagement.DTOs.JobCandidates;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Services;

public class ScheduleInterviewService : IScheduleInterviewRepository
{
    private readonly ApplicationContext applicationContext;
    private readonly IJobCandidateRepository jobCandidateRepository;

    public ScheduleInterviewService(ApplicationContext applicationContext,IJobCandidateRepository jobCandidateRepository){
        this.applicationContext = applicationContext;
        this.jobCandidateRepository = jobCandidateRepository;
    }

    public async Task<ScheduledInterview> AddScheduledInterview(JobCandidate jobCandidate,NewScheduledInterviewDto scheduledInterviewDto)
    {
        if(!jobCandidate.isFiltered){
            throw new Exception("Job Canidatie is not filtered for this job...!");
        }
        
        //Validating interviewType
        var interviewType = await applicationContext.InterviewTypes.FindAsync(scheduledInterviewDto.interviewTypeId);
        if(interviewType == null){
            throw new Exception("Interview type not found...!");
        }
        
        //Validate Date is not before current Date
        if(scheduledInterviewDto.interviewDate < DateTime.Now){
            throw new Exception("Date Can't be before today's date...!");
        }

        var scheduledInterview = new ScheduledInterview{
            interviewDate = scheduledInterviewDto.interviewDate,
            jobCandidateId = jobCandidate.id,
            jobOpeningId = jobCandidate.jobOpeningId,
            candidateId = jobCandidate.candidateId,
            jobCandidate = jobCandidate,
            interviewTypeId = scheduledInterviewDto.interviewTypeId,
        };
        //Validating round handlers
        foreach(var employeeId in scheduledInterviewDto.roundHandlersIds){
            var employee = await applicationContext.Employees.FindAsync(employeeId);
            if(employee == null){
                throw new Exception($"Couldn't found the employee with id: {employeeId}");
            }
            scheduledInterview.roundHandlers.Add(new RoundHandler{
                scheduledInterviewJobOpeningId =  jobCandidate.jobOpeningId,
                scheduledInterviewCandidateId = jobCandidate.candidateId,
                scheduledInterviewInterviewTypeId = scheduledInterviewDto.interviewTypeId,
                employeeId = employeeId
            });
        }
        var result =  await applicationContext.ScheduledInterviews.AddAsync(scheduledInterview);
        await applicationContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<ScheduledInterview> UpdateScheduledInterview(int jobCandidateId,UpdateScheduledInterviewDto scheduledInterviewDto)
    {
        var existingScheduledInterview = await applicationContext.ScheduledInterviews.Include(es=>es.roundHandlers).Include(es=>es.interviewType).Include(es=>es.jobCandidate).FirstOrDefaultAsync(si => si.jobCandidateId == jobCandidateId);
        if(existingScheduledInterview == null || existingScheduledInterview.isCleared){
            throw new Exception("Either the interview has been cleared or it doesn't exists");
        }

        // Validating interviewType
        // var interviewType = await applicationContext.InterviewTypes.FindAsync(scheduledInterviewDto.interviewTypeId);
        // if(interviewType == null){
        //     throw new Exception("Interview type not found...!");
        // }
        
        //Validate Date is not before current Date
        if(scheduledInterviewDto.interviewDate < DateTime.Now){
            throw new Exception("Date Can't be before today's date...!");
        }
    

        //Validating round handlers
        List<RoundHandler> updatedRoundHandlers = new List<RoundHandler>();
        foreach(var employeeId in scheduledInterviewDto.roundHandlersIds){
            var employee = await applicationContext.Employees.FindAsync(employeeId);
            if(employee == null){
                throw new Exception($"Couldn't found the employee with id: {employeeId}");
            }
            updatedRoundHandlers.Add(new RoundHandler{
                scheduledInterviewJobOpeningId =  existingScheduledInterview.jobCandidateId,
                scheduledInterviewCandidateId = existingScheduledInterview.candidateId,
                scheduledInterviewInterviewTypeId = existingScheduledInterview.interviewTypeId,
                employeeId = employeeId
            });
        }

        //Updating values
        // existingScheduledInterview.interviewTypeId = scheduledInterviewDto.interviewTypeId;     
        existingScheduledInterview.interviewDate = scheduledInterviewDto.interviewDate;
        existingScheduledInterview.roundHandlers = updatedRoundHandlers;
        existingScheduledInterview.isCleared = scheduledInterviewDto.isCleared;
        existingScheduledInterview.rating = scheduledInterviewDto.rating;
        existingScheduledInterview.feedback = scheduledInterviewDto.feedback;

        await applicationContext.SaveChangesAsync();
        return existingScheduledInterview;
    }


    public async Task<InterviewType> GetInterviewTypeById(int interviewTypeId)
    {
        var result = await applicationContext.InterviewTypes.FindAsync(interviewTypeId);
        if(result == null){
            throw new Exception($"Interview Type not found for id : {interviewTypeId}");
        }
        return result;
    }

    public async Task<List<string>> GetRounhandlerList(List<string> roundHandlerIds)
    {
        List<string> roundHandlerList = new List<string>();
        foreach(var employeeId in roundHandlerIds){
            var employee = await applicationContext.Employees.FindAsync(employeeId);
            if(employee == null){
                throw new Exception($"Couldn't found the employee with id: {employeeId}");
            }
            roundHandlerList.Add(employee.UserName+" : "+employee.firstName+employee.lastName);
        }
        return roundHandlerList;
    }
}