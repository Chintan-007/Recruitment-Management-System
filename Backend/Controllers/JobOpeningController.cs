using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagement.DTOs.JobCandidates;
using RecruitmentManagement.DTOs.JobOpening;
using RecruitmentManagement.Mappers;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobOpeningController : ControllerBase
{
    private readonly IJobOpeningRepository jobOpeningRepository;
    public JobOpeningController(IJobOpeningRepository jobOpeningRepository){
        this.jobOpeningRepository = jobOpeningRepository;
    }

    // Create
    // [HttpPost]
    // [Authorize(Roles ="Organisation")]
    // public async Task<ActionResult<AddedJobOpeningDto>> AddJobOpening(CreateJobOpeningDto createJobOpeningDto){
    //     try{
    //         if(!ModelState.IsValid){
    //             return BadRequest();
    //         }
    //             var createdJobOpening = await jobOpeningRepository.CreateJobOpening(createJobOpeningDto);           
    //             return createdJobOpening.ModelToAddedJobOpeningDto();
    //     }
    //     catch(Exception e){
    //         return StatusCode(StatusCodes.Status500InternalServerError,e);
    //     }
    // }




    // // Read
    // [HttpGet("id:int")]
    // [Authorize(Roles ="Organisation")]
    // public async Task<ActionResult<AddedJobOpeningDto>> GetJobOpeningById(int id){
    //     try{
    //         var jobOpening =  await jobOpeningRepository.GetJobOpeningById(id);
    //         if(jobOpening == null){
    //             return NotFound();
    //         }
    //         // foreach(var js in jobOpening.jobSkills){
    //         //     Console.WriteLine("==============JobSkill is: "+js.skill);
    //         // }
        
    //         return jobOpening.ModelToAddedJobOpeningDto();
    //     }
    //     catch(Exception e){
    //         return StatusCode(StatusCodes.Status500InternalServerError,e);
    //     }
    // }

    // [HttpGet]
    // [Authorize(Roles ="Organisation")]
    // public async Task<ActionResult> GetJobOpenings(){
    //     try{
    //         var jobOpenings =  await jobOpeningRepository.GetJobOpenings();
    //         if(jobOpenings == null){
    //             return NotFound();
    //         }
    //         return Ok(jobOpenings.Select(jo=> jo.ModelToAddedJobOpeningDto()));
    //     }
    //     catch(Exception e){
    //         return StatusCode(StatusCodes.Status500InternalServerError,e);
    //     }
    // }


    // [HttpPut]
    // [Authorize(Roles ="Organisation")]
    // public async Task<ActionResult<AddedJobOpeningDto>> UpdateJobOpening(int jobOpeningId,UpdateJobOpeningDto updateJobOpeningDto){
    //     try{
    //         if(!ModelState.IsValid){
    //             return BadRequest();
    //         }
    //         var jobOpening =  await jobOpeningRepository.GetJobOpeningById(jobOpeningId);
    //         if(jobOpening == null){
    //             return NotFound($"Job opening with id: {jobOpeningId} not found");
    //         }
    //         var updatedJobOpening = await jobOpeningRepository.UpdateJobOpeningById(jobOpeningId,updateJobOpeningDto);           
    //         return updatedJobOpening.ModelToAddedJobOpeningDto();
    //     }
    //     catch(Exception e){
    //         return StatusCode(StatusCodes.Status500InternalServerError,e);
    //     }
    // }

}