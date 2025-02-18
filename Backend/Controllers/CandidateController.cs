using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagement.DTOs.CandidateDocs;
using RecruitmentManagement.DTOs.JobCandidates;
using RecruitmentManagement.Mappers;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CandidateController : ControllerBase
{
    private readonly ICandidateDocsRepository candidateDocsRepository;
    private readonly IDocumentTypeRepository documentTypeRepository;
    private readonly IJobOpeningRepository jobOpeningRepository;
    private readonly IJobCandidateRepository jobCandidateRepository;


    public CandidateController(ICandidateDocsRepository candidateDocsRepository,IDocumentTypeRepository documentTypeRepository,
                                IJobOpeningRepository jobOpeningRepository,IJobCandidateRepository jobCandidateRepository){
        this.candidateDocsRepository = candidateDocsRepository;
        this.documentTypeRepository = documentTypeRepository;
        this.jobOpeningRepository = jobOpeningRepository;
        this.jobCandidateRepository = jobCandidateRepository;
    }



//====================================================Create===================================================
    [Authorize(Roles ="Candidate")]
    [HttpPost("candidate-documents/upload")]
    public async Task<ActionResult<AddedCandidateDocsDto>> AddCandidateDocs(NewCandidateDocsDto newCandidateDocsDto){
        try{
            if(!ModelState.IsValid){
                return BadRequest();
            }

            var candidateId = User.FindFirst("id")?.Value;
            if(candidateId == null){
                return StatusCode(StatusCodes.Status401Unauthorized,"Candidate id not found in the token.");
            }

            //Checking If the document is already uploaded
            foreach(var docData in newCandidateDocsDto.documentDatas){
                var existiondDoc = await candidateDocsRepository.GetCandidateDocByCandidateIdandDocId(candidateId,docData.documentTypeId);
                if(existiondDoc != null){
                    return StatusCode(StatusCodes.Status208AlreadyReported,$"The document having id ${docData.documentTypeId} has already been submitted.");
                }
            }

            var updatedCandidate = await candidateDocsRepository.AddCandidateDocs(newCandidateDocsDto);
           
           //Getting document dtails for dto
            List<DisplayDocs> documentDetlails = new List<DisplayDocs>(); 
            foreach(var candidateDoc in updatedCandidate.candidateDocs){
                var doctype = await documentTypeRepository.GetDocumentTypeById(candidateDoc.documentTypeId);
                if(doctype == null){
                    throw new Exception("Document type not found");
                }
                
                documentDetlails.Add(new DisplayDocs{
                    documentName = doctype.documentType,
                    link = candidateDoc.documentLink
                });
            }
            updatedCandidate.AddedDocsModelToDto(documentDetlails);
            return Ok();
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }

//Job Candidates(Applying for job)
    [HttpPost("job-opening/{jobOpeningId:int}/apply")]
    [Authorize(Roles ="Candidate")]
    public async Task<ActionResult<AfterUpdateJobCandidateDto>> AddJobCandidate(int jobOpeningId){
        try{
            
            
            var candidateId = User.FindFirst("id")?.Value;
            if(candidateId == null){
                return StatusCode(StatusCodes.Status401Unauthorized,"Candidate id not found in the token.");
            }

            var jobOpening =  await jobOpeningRepository.GetJobOpeningById(jobOpeningId);
            if(jobOpening == null){
                return NotFound($"Job opening with id: {jobOpeningId} not found");
            }

            if(!String.Equals(jobOpening.jobStatus.status,"Open")){
                return BadRequest("This job is not open");
            }

            var existiondDoc = await candidateDocsRepository.GetCandidateDocsByCandidateId(candidateId);
                if(existiondDoc == null || !existiondDoc.Any()){
                    return StatusCode(StatusCodes.Status401Unauthorized,"You haven't uploaded any documents");
                }

            if(await jobCandidateRepository.GetJobCandidateByjobOpeningIdAndcanidateId(jobOpeningId,candidateId) != null){
                return StatusCode(StatusCodes.Status208AlreadyReported,"Already applied for this job");
            }

            var jobCandiate = await jobOpeningRepository.AddJobCandidateByCandidate(jobOpeningId,candidateId);           
             
             return jobCandiate.ModelToUpdatedJobCandidateDto();
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }


    [Authorize(Roles ="Candidate")]
    [HttpPost("selected-candidate-documents/upload")]
    public async Task<ActionResult<AddedCandidateDocsDto>> AddSelectedCandidateDocs(NewCandidateDocsDto newCandidateDocsDto){
        try{
            if(!ModelState.IsValid){
                return BadRequest();
            }

            var candidateId = User.FindFirst("id")?.Value;
            if(candidateId == null){
                return StatusCode(StatusCodes.Status401Unauthorized,"Candidate id not found in the token.");
            }

            var selectedJobCandidate = await jobCandidateRepository.GetSelectedJobCandidateByCandidateId(candidateId);
            if(selectedJobCandidate == null){
                return StatusCode(StatusCodes.Status405MethodNotAllowed,"You can not upload document without getting selected for any job..!");  
            }

            //Checking If the document is already uploaded
            foreach(var docData in newCandidateDocsDto.documentDatas){
                var existiondDoc = await candidateDocsRepository.GetCandidateDocByCandidateIdandDocId(candidateId,docData.documentTypeId);
                if(existiondDoc != null){
                    return StatusCode(StatusCodes.Status208AlreadyReported,$"The document having id ${docData.documentTypeId} has already been submitted.");
                }
            }

            var updatedCandidate = await candidateDocsRepository.AddCandidateDocs(newCandidateDocsDto);
           
           //Getting document dtails for dto
            List<DisplayDocs> documentDetlails = new List<DisplayDocs>(); 
            foreach(var candidateDoc in updatedCandidate.candidateDocs){
                var doctype = await documentTypeRepository.GetDocumentTypeById(candidateDoc.documentTypeId);
                if(doctype == null){
                    throw new Exception("Document type not found");
                }
                
                documentDetlails.Add(new DisplayDocs{
                    documentName = doctype.documentType,
                    link = candidateDoc.documentLink
                });
            }
            updatedCandidate.AddedDocsModelToDto(documentDetlails);
            return Ok();
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }
//============================================Read=========================================================

// See all the job openings
    [HttpGet("job-openings")]
    [Authorize(Roles ="Candidate")]
    public async Task<ActionResult> GetJobOpenings(){
        try{
            var candidateId = User.FindFirst("id")?.Value;
            if(candidateId == null){
                return StatusCode(StatusCodes.Status401Unauthorized,"Candidate id not found in the token.");
            }
            var jobOpenings =  await jobOpeningRepository.GetJobOpenings();
            if(jobOpenings == null){
                return NotFound();
            }
            return Ok(jobOpenings.Select(jo=> jo.ModelToAddedJobOpeningDto()));
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }

//See applied jobs
    [HttpGet("job-openings/applied-jobs")]
    [Authorize(Roles ="Candidate")]
    public async Task<ActionResult> GetAppliedJobs(){
        try{
            var candidateId = User.FindFirst("id")?.Value;
            if(candidateId == null){
                return StatusCode(StatusCodes.Status401Unauthorized,"Candidate id not found in the token.");
            }
            var jobOpenings =  await jobOpeningRepository.GetCandidateJobOpenings(candidateId);
            if(jobOpenings == null){
                return NotFound();
            }
            return Ok(jobOpenings.Select(jo=> jo.ModelToAddedJobOpeningDto()));
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }
}