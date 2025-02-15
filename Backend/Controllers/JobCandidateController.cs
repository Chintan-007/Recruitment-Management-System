using Microsoft.AspNetCore.Mvc;
using RecruitmentManagement.DTOs.JobCandidates;
using RecruitmentManagement.DTOs.JobOpening;
using RecruitmentManagement.Mappers;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobCandidateController : ControllerBase
{
    private readonly IJobOpeningRepository jobOpeningRepository;
    private readonly IJobCandidateRepository jobCandidateRepository;
    public JobCandidateController(IJobOpeningRepository jobOpeningRepository, IJobCandidateRepository jobCandidateRepository){
        this.jobOpeningRepository = jobOpeningRepository;
        this.jobCandidateRepository = jobCandidateRepository;
    }


    //Create Job Candidates
    [HttpPost("{jobOpeningId}")]
    public async Task<ActionResult<AddedJobCandidateDto>> AddJobCandidate(int jobOpeningId,NewJobCandidateDto jobCandidateDto){
        try{
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var jobOpening =  await jobOpeningRepository.GetJobOpeningById(jobOpeningId);
            if(jobOpening == null){
                return NotFound($"Job opening with id: {jobOpeningId} not found");
            }
            var updatedJobOpening = await jobOpeningRepository.AddJobCandidate(jobOpeningId,jobCandidateDto);           
            return updatedJobOpening.ModelToAddedJobCandidateDto();
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }

    [HttpPut("{jobCandidateId}")]
    public async Task<ActionResult<AfterUpdateJobCandidateDto>> UpdateJobCandidate(int jobCandidateId,UpdateJobCandidateDto jobCandidateDto){
        try{
            if(!ModelState.IsValid){
                return BadRequest();
            }
            
            var updatedJobCandidate = await jobCandidateRepository.UpdateJobCandidateById(jobCandidateId,jobCandidateDto);
            return updatedJobCandidate.ModelToUpdatedJobCandidateDto();
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }

}