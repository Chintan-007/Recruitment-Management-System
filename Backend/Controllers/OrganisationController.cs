using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RecruitmentManagement.DTOs.CandidateDocs;
using RecruitmentManagement.DTOs.JobCandidates;
using RecruitmentManagement.DTOs.JobOpening;
using RecruitmentManagement.DTOs.Organisations;
using RecruitmentManagement.Mappers;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;
using RecruitmentManagement.Services;

namespace RecruitmentManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize(Roles ="Organisation")]
public class OrganisationController : ControllerBase
{

    private readonly IOrganisationRepository organisationRepository;
    private readonly IJobOpeningRepository jobOpeningRepository;
    private readonly IJobCandidateRepository jobCandidateRepository;
    private readonly IScheduleInterviewRepository scheduleInterviewRepository;
     private readonly ICandidateDocsRepository candidateDocsRepository;


    public OrganisationController(IOrganisationRepository organisationRepository,IJobOpeningRepository jobOpeningRepository,
                                IJobCandidateRepository jobCandidateRepository,IScheduleInterviewRepository scheduleInterviewRepository,
                                ICandidateDocsRepository candidateDocsRepository){
        this.jobOpeningRepository = jobOpeningRepository;
        this.organisationRepository = organisationRepository;
        this.jobCandidateRepository = jobCandidateRepository;
        this.scheduleInterviewRepository = scheduleInterviewRepository;
        this.candidateDocsRepository = candidateDocsRepository;

    }



//===========================================Create===============================================
    //Job opening
    [HttpPost("job-opening/create")]
    [Authorize(Roles ="Organisation")]
    public async Task<ActionResult<AddedJobOpeningDto>> AddJobOpening(CreateJobOpeningDto createJobOpeningDto){
        try{
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var organisationId = User.FindFirst("id")?.Value;
            if(organisationId == null){
                return StatusCode(StatusCodes.Status401Unauthorized,"Organisatoin id not found in the token.");
            }
            var createdJobOpening = await jobOpeningRepository.CreateJobOpening(createJobOpeningDto,organisationId);           
            return createdJobOpening.ModelToAddedJobOpeningDto();
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }

    //Job Candidates
    [HttpPost("job-opening/{jobOpeningId:int}/add-candidate")]
    [Authorize(Roles ="Organisation")]
    public async Task<ActionResult<AddedJobCandidateDto>> AddJobCandidate(int jobOpeningId,NewJobCandidateDto jobCandidateDto){
        try{
            if(!ModelState.IsValid){
                return BadRequest();
            }
            
            var organisationId = User.FindFirst("id")?.Value;
            if(organisationId == null){
                return StatusCode(StatusCodes.Status401Unauthorized,"Organisatoin id not found in the token.");
            }

            var jobOpening =  await jobOpeningRepository.GetJobOpeningById(jobOpeningId);
            if(jobOpening == null){
                return NotFound($"Job opening with id: {jobOpeningId} not found");
            }
            
            if(jobOpening.organisationId != organisationId){
                return Forbid("You do not have access to this job opening...!");
            }

            var updatedJobOpening = await jobOpeningRepository.AddJobCandidateByOrganisation(jobOpeningId,jobCandidateDto);           
            return updatedJobOpening.ModelToAddedJobCandidateDto();
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }


    //Schedule interview
  [HttpPost("job-candidate/{jobCandidateId:int}/schedule-interview")]
  [Authorize(Roles ="Organisation")]
    public async Task<ActionResult<AddedScheduledInterviewDto>> AddScheduledInterview(int jobCandidateId, NewScheduledInterviewDto scheduledInterviewDto){
        try{
            if(!ModelState.IsValid){
                return BadRequest();
            }

            var organisationId = User.FindFirst("id")?.Value;
            if(organisationId == null){
                return StatusCode(StatusCodes.Status401Unauthorized,"Organisatoin id not found in the token.");
            }

            var jobCandidate = await jobCandidateRepository.GetJobCandidateById(jobCandidateId);
            var jobOpeningId = jobCandidate.jobOpeningId;

            var jobOpening =  await jobOpeningRepository.GetJobOpeningById(jobOpeningId);
            if(jobOpening == null){
                return NotFound($"Job opening with id: {jobOpeningId} not found");
            }
            
            if(jobOpening.organisationId != organisationId){
                return Forbid("You do not have access to this job opening...!");
            }


            var scheduledInterview = await scheduleInterviewRepository.AddScheduledInterview(jobCandidate,scheduledInterviewDto,organisationId);
            var interviewType = await scheduleInterviewRepository.GetInterviewTypeById(scheduledInterviewDto.interviewTypeId);
            List<string> roundHandlers = await scheduleInterviewRepository.GetRounhandlerList(scheduledInterviewDto.roundHandlersIds);
            return scheduledInterview.ModelToAddedScheduldeInterviewDto(interviewType.interviewType,roundHandlers);
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }



    //=====================================================Read=================================================
    [HttpGet("job-openings/{jobOpeningId:int}")]
    [Authorize(Roles ="Organisation")]
    public async Task<ActionResult<AddedJobOpeningDto>> GetJobOpeningById(int jobOpeningId){
        try{
            var organisationId = User.FindFirst("id")?.Value;
            if(organisationId == null){
                return StatusCode(StatusCodes.Status401Unauthorized,"Organisatoin id not found in the token.");
            }
            var jobOpening =  await jobOpeningRepository.GetJobOpeningById(jobOpeningId);
            if(jobOpening == null){
                return NotFound();
            }

            if(jobOpening.organisationId != organisationId){
                return Forbid("You do not have access to this job opening...!");
            }
        
            return jobOpening.ModelToAddedJobOpeningDto();
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }

    [HttpGet("job-openings")]
    [Authorize(Roles ="Organisation")]
    public async Task<ActionResult> GetJobOpenings(){
        try{
            var organisationId = User.FindFirst("id")?.Value;
            if(organisationId == null){
                return StatusCode(StatusCodes.Status401Unauthorized,"Organisatoin id not found in the token.");
            }

            var jobOpenings =  await jobOpeningRepository.GetOrganisationJobOpenings(organisationId);
            if(jobOpenings == null){
                return NotFound();
            }
        
            return Ok(jobOpenings.Select(jo=> jo.ModelToAddedJobOpeningDto()));
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }

    [HttpGet("job-opening/{jobOpeningId:int}/candidates")]
    [Authorize(Roles ="Organisation")]
    public async Task<ActionResult> GetJobCandidates(int jobOpeningId){
        try{
            var organisationId = User.FindFirst("id")?.Value;
            if(organisationId == null){
                return StatusCode(StatusCodes.Status401Unauthorized,"Organisatoin id not found in the token.");
            }
            var jobOpening =  await jobOpeningRepository.GetJobOpeningById(jobOpeningId);
            if(jobOpening == null){
                return NotFound();
            }

            if(jobOpening.organisationId != organisationId){
                return Forbid("You do not have access to this job opening...!");
            }

            var jobCandidates =  await jobCandidateRepository.GetJobCanidatesByJobOpeningId(jobOpeningId);

            List<CandidateInfoDto> candidates = new List<CandidateInfoDto>();
            foreach(var jobCand in jobCandidates){
                if(jobCand != null)
                candidates.Add(jobCand.ModelToGetCanidateForOrganisation());
            }
            return Ok(candidates);
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }

    [HttpGet("job-opening/{jobOpeningId:int}/candidate/{candidateId}/documents")]
    [Authorize(Roles ="Organisation")]
    public async Task<ActionResult> GetJobCandidatesDocs(int jobOpeningId,string candidateId){
        try{
            var organisationId = User.FindFirst("id")?.Value;
            if(organisationId == null){
                return StatusCode(StatusCodes.Status401Unauthorized,"Organisatoin id not found in the token.");
            }
            var jobOpening =  await jobOpeningRepository.GetJobOpeningById(jobOpeningId);
            if(jobOpening == null){
                return NotFound();
            }

            if(jobOpening.organisationId != organisationId){
                return Forbid("You do not have access to this job opening...!");
            }

            var jobCandidates =  await jobCandidateRepository.GetJobCanidatesByJobOpeningId(jobOpeningId);

            var candidate = jobCandidates.Select(jc=>String.Equals(jc.candidateId,candidateId));
            if(candidate == null){
                return NotFound($"The candidate with id :{candidateId} not found in your job opening");
            }    


            var docs = await candidateDocsRepository.GetCandidateDocsByCandidateId(candidateId);

            List<DocumentData> documentDatas = new List<DocumentData>();
            //Need to be moved to mappers
            foreach(var doc in docs){
                documentDatas.Add(new DocumentData{
                     documentTypeId = doc.documentTypeId,
                     documentLink = doc.documentLink
                });
            }
        
            return Ok(documentDatas);
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }

    //==========================================================Update======================================================= 
    [HttpPut("job-opening/{jobOpeningId:int}/update")]
    [Authorize(Roles ="Organisation")]
    public async Task<ActionResult<AddedJobOpeningDto>> UpdateJobOpening(int jobOpeningId,UpdateJobOpeningDto updateJobOpeningDto){
        try{
            if(!ModelState.IsValid){
                return BadRequest();
            }

            var organisationId = User.FindFirst("id")?.Value;
            if(organisationId == null){
                return StatusCode(StatusCodes.Status401Unauthorized,"Organisatoin id not found in the token.");
            }
            
            var jobOpening =  await jobOpeningRepository.GetJobOpeningById(jobOpeningId);
            if(jobOpening == null){
                return NotFound($"Job opening with id: {jobOpeningId} not found");
            }

            if(jobOpening.organisationId != organisationId){
                return Forbid("You do not have access to this job opening...!");
            }
            var updatedJobOpening = await jobOpeningRepository.UpdateJobOpeningById(jobOpeningId,updateJobOpeningDto,organisationId);           
            return updatedJobOpening.ModelToAddedJobOpeningDto();
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }

    //Job-Candidate update
    [HttpPut("job-opening/{jobOpeningId:int}/update-candidate")]
    [Authorize(Roles ="Organisation")]
    public async Task<ActionResult<AfterUpdateJobCandidateDto>> UpdateJobCandidate(int jobOpeningId,UpdateJobCandidateDto jobCandidateDto){
        try{
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var organisationId = User.FindFirst("id")?.Value;
            if(organisationId == null){
                return StatusCode(StatusCodes.Status401Unauthorized,"Organisatoin id not found in the token.");
            }

            var jobCandidate =  await jobCandidateRepository.GetJobCandidateByjobOpeningIdAndcanidateId(jobOpeningId,jobCandidateDto.candidateId);
            if(jobCandidate == null){
                return NotFound("Job candidate not found");
            }
            
            var jobOpening = await jobOpeningRepository.GetJobOpeningById(jobOpeningId);

            if(jobOpening.organisationId != organisationId){
                return Forbid("You do not have access to this job opening...!");
            }

            var updatedJobCandidate = await jobCandidateRepository.UpdateJobCandidateById(jobCandidate.id,jobCandidateDto);
            return updatedJobCandidate.ModelToUpdatedJobCandidateDto();
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }


    //Schedule interview update
    [HttpPut("job-candidate/{jobCandidateId:int}/schedule-interview/update")]
    [Authorize(Roles ="Organisation")]
    public async Task<ActionResult<AfterUpdateScheduleInterviewDto>> UpdateScheduledInterview(int jobCandidateId,UpdateScheduledInterviewDto scheduledInterviewDto){
        try{
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var organisationId = User.FindFirst("id")?.Value;
            if(organisationId == null){
                return StatusCode(StatusCodes.Status401Unauthorized,"Organisatoin id not found in the token.");
            }

            var jobCandidate = await jobCandidateRepository.GetJobCandidateById(jobCandidateId);
            if(jobCandidate == null){
                return NotFound("Job Candidate not found");
            }

            var jobOpeningId = jobCandidate.jobOpeningId;

            var jobOpening =  await jobOpeningRepository.GetJobOpeningById(jobOpeningId);
            if(jobOpening == null){
                return NotFound($"Job opening with id: {jobOpeningId} not found");
            }
            
            if(jobOpening.organisationId != organisationId){
                return Forbid("You do not have access to this job opening...!");
            }

            var updatedScheduledInterview = await scheduleInterviewRepository.UpdateScheduledInterview(jobCandidateId,scheduledInterviewDto);
            var interviewType = await scheduleInterviewRepository.GetInterviewTypeById(updatedScheduledInterview.interviewTypeId);
            List<string> roundHandlers = await scheduleInterviewRepository.GetRounhandlerList(scheduledInterviewDto.roundHandlersIds);
            var unClearedInterview = await scheduleInterviewRepository.GetUnclearedInterviewByJobCandidateId(jobCandidateId);
            if(unClearedInterview == null){
                await jobOpeningRepository.MarkCandidateSelected(jobCandidate);
            }
            return updatedScheduledInterview.ModelToUpdatedScheduldeInterviewDto(interviewType.interviewType,roundHandlers);
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }

//======================================================Delete=======================================================================
    [HttpDelete("job-opening/{jobOpeningId:int}")]
    [Authorize(Roles ="Organisation")]
    public async Task<ActionResult> DeleteJobOpening(int jobOpeningId){
        try{
            var organisationId = User.FindFirst("id")?.Value;
            if(organisationId == null){
                return StatusCode(StatusCodes.Status401Unauthorized,"Organisatoin id not found in the token.");
            }
            
            var jobOpening =  await jobOpeningRepository.GetJobOpeningById(jobOpeningId);
            if(jobOpening == null){
                return NotFound($"Job opening with id: {jobOpeningId} not found");
            }

            if(jobOpening.organisationId != organisationId){
                return Forbid("You do not have access to this job opening...!");
            }
            await jobOpeningRepository.DeleteJobOpeningById(jobOpeningId);
            return Ok("Job Opening Deleted...!");
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }

}