using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagement.DTOs;
using RecruitmentManagement.Mappers;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InterviewRoundController : ControllerBase
{
    private readonly IScheduleInterviewRepository scheduleInterviewRepository;
    private readonly IInterviewRoundRepository interviewRoundRepository;
    private readonly IInterviewTypeRepository interviewTypeRepository;
    
    public InterviewRoundController(IScheduleInterviewRepository scheduleInterviewRepository
                                    ,IInterviewTypeRepository interviewTypeRepository
                                    ,IInterviewRoundRepository interviewRoundRepository){
        this.scheduleInterviewRepository = scheduleInterviewRepository;
        this.interviewRoundRepository = interviewRoundRepository;
        this.interviewTypeRepository = interviewTypeRepository;
    }


    [HttpPut("{scheduledInterviewId:int}/update")]
    [Authorize (Roles="Employee")]
    public async Task<ActionResult<GetInterviewRoundDto>> AddFeedBack(int scheduledInterviewId, AddFeedBackDto addFeedBackDto){
        try{
            if(!ModelState.IsValid){
                return BadRequest();
            }
            
            var employeeId = User.FindFirst("id")?.Value;
            if(employeeId == null){
                return StatusCode(StatusCodes.Status401Unauthorized,"Employee id not found in the token.");
            }

            var interviewRound =  await interviewRoundRepository.GetInterviewRoundByEmployeeIdAndInterviewId(scheduledInterviewId,employeeId);
            if(interviewRound == null){
                return StatusCode(StatusCodes.Status404NotFound,"Interview Round not found.");
            }

            var updatedInterviewRound = await interviewRoundRepository.AddFeedBack(addFeedBackDto,interviewRound);           
            var interviewType = await interviewTypeRepository.GetInterviewTypeById(updatedInterviewRound.scheduledInterviewInterviewTypeId);
            var interviewer = await interviewRoundRepository.GetInterviewerById(employeeId);
            var candidate = await interviewRoundRepository.GetCandidateById(updatedInterviewRound.scheduledInterviewCandidateId);
            if(interviewer == null || interviewType == null || candidate == null){
                 return StatusCode(StatusCodes.Status204NoContent,"Interview type or interviewer or candidate not found"); 
            }
            return updatedInterviewRound.ModelToGetInterviewRoundDto(candidate.UserName,interviewType.interviewType,interviewer.UserName);
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }
}