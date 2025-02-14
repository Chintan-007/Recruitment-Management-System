using Microsoft.AspNetCore.Mvc;
using RecruitmentManagement.DTOs.JobCandidates;
using RecruitmentManagement.Mappers;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleInterviewController : ControllerBase
{
    private readonly IScheduleInterviewRepository scheduleInterviewRepository;
    public ScheduleInterviewController(IScheduleInterviewRepository scheduleInterviewRepository){
        this.scheduleInterviewRepository = scheduleInterviewRepository;
    }

    
    //Create
    [HttpPost]
    public async Task<ActionResult<AddedScheduledInterviewDto>> AddScheduledInterview(NewScheduledInterviewDto scheduledInterviewDto){
        try{
            if(!ModelState.IsValid){
                return BadRequest();
            }

            var scheduledInterview = await scheduleInterviewRepository.AddScheduledInterview(scheduledInterviewDto);
            var interviewType = await scheduleInterviewRepository.GetInterviewTypeById(scheduledInterviewDto.interviewTypeId);
            List<string> roundHandlers = await scheduleInterviewRepository.GetRounhandlerList(scheduledInterviewDto.roundHandlersIds);
            return scheduledInterview.ModelToAddedScheduldeInterviewDto(interviewType.interviewType,roundHandlers);
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }

    //Update
    [HttpPut]
    public async Task<ActionResult<AfterUpdateScheduleInterviewDto>> UpdateScheduledInterview(int scheduledInterviewId,UpdateScheduledInterviewDto scheduledInterviewDto){
        try{
            if(!ModelState.IsValid){
                return BadRequest();
            }

            var updatedScheduledInterview = await scheduleInterviewRepository.UpdateScheduledInterview(scheduledInterviewId,scheduledInterviewDto);
            var interviewType = await scheduleInterviewRepository.GetInterviewTypeById(updatedScheduledInterview.interviewTypeId);
            List<string> roundHandlers = await scheduleInterviewRepository.GetRounhandlerList(scheduledInterviewDto.roundHandlersIds);
            return updatedScheduledInterview.ModelToUpdatedScheduldeInterviewDto(interviewType.interviewType,roundHandlers);
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }
}