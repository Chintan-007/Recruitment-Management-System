using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagement.DTOs.JobOpening;
using RecruitmentManagement.Mappers;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;
using RecruitmentManagement.Service;

namespace RecruitmentManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobOpeningController : ControllerBase
{
    private readonly IJobOpeningRepository jobOpeningRepository;
    public JobOpeningController(IJobOpeningRepository jobOpeningRepository){
        this.jobOpeningRepository = jobOpeningRepository;
    }

    //Create
    [HttpPost]
    public async Task<ActionResult<AddedJobOpeningDto>> AddJobOpening(CreateJobOpeningDto createJobOpeningDto){
        try{
            if(!ModelState.IsValid){
                return BadRequest();
            }
                var createdJobOpening = await jobOpeningRepository.CreateJobOpening(createJobOpeningDto);           
                return createdJobOpening.ModelToAddedJobOpeningDto();
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }




    //Read
    [HttpGet]
    public async Task<ActionResult<GetJobOpeningDto>> GetJobOpeningById(int id){
        try{
            var jobOpening =  await jobOpeningRepository.GetJobOpeningById(id);
            if(jobOpening == null){
                return NotFound();
            }
            if(jobOpening.jobSkills == null){
                return NotFound();
            }
            return jobOpening.ModelToGetJobOpeningDto();
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }




    [HttpPut]
    public async Task<ActionResult<AddedJobOpeningDto>> UpdateJobOpening(int jobOpeningId,CreateJobOpeningDto createJobOpeningDto){
        try{
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var jobOpening =  await jobOpeningRepository.GetJobOpeningById(jobOpeningId);
            if(jobOpening == null){
                return NotFound($"Job opening with id: {jobOpeningId} not found");
            }
            var updatedJobOpening = await jobOpeningRepository.UpdateJobOpeningById(jobOpeningId,createJobOpeningDto);           
            return updatedJobOpening.ModelToAddedJobOpeningDto();
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }
}