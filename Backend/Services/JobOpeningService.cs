using Microsoft.EntityFrameworkCore;
using RecruitmentManagement.DTOs.JobCandidates;
using RecruitmentManagement.DTOs.JobOpening;
using RecruitmentManagement.Mappers;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Services;

public class JobOpeningService : IJobOpeningRepository
{
    private readonly ApplicationContext applicationContext;
    private readonly IPositionRepository positionRepository;
    private readonly IOrganisationRepository organisationRepository;
   
    public JobOpeningService(ApplicationContext applicationContext, IPositionRepository positionRepository, IOrganisationRepository organisationRepository){
        this.applicationContext = applicationContext;
        this.positionRepository = positionRepository;
        this.organisationRepository = organisationRepository;
    }   


//==========================================================Create======================================================================
    public async Task<JobOpening> CreateJobOpening(CreateJobOpeningDto createJobOpeningDto, string organisationId)
    {
        //Validationg all the data respected to their Id's
        var position = await positionRepository.GetPositionById(createJobOpeningDto.positionId);
        if(position == null){
            throw new Exception("Position Not Found !");
        }
        
        var organisation = await organisationRepository.GetOrganisationById(organisationId);
        if(organisation == null){
            throw new Exception("Organisatoin Not Found");
        }
        
        var jobStatus = await applicationContext.JobStatuses.FindAsync(createJobOpeningDto.jobStatusId);
        if(jobStatus == null){
            throw new Exception("Invalid JobStatus Id");
        }

        var jobType = await applicationContext.JobTypes.FindAsync(createJobOpeningDto.jobTypeId);
        if(jobType == null){
            throw new Exception("Invalid JobType Id");
        }


        //Process for validationg and adding jobSkills
        var jobOpeningModel = createJobOpeningDto.DtoToModel(position,organisation,jobType,jobStatus);
        var jobOpeningId = jobOpeningModel.JobOpeningId;

        foreach(var js in createJobOpeningDto.jobSkills){
            var skill = await applicationContext.Skills.FindAsync(js.skillId);
            if(skill == null){
                throw new Exception("Invalid Skill id: "+js.skillId);
            }
            jobOpeningModel.jobSkills.Add(new JobSkill{
                jobOpeningId = jobOpeningId,
                skillId = js.skillId,
                isRequired = js.isRequired,
            });
        }

        var result = await applicationContext.JobOpenings.AddAsync(jobOpeningModel);        
        await applicationContext.SaveChangesAsync();
        return result.Entity;
    }


public async Task<JobOpening> AddJobCandidateByOrganisation(int jobOpeningId,NewJobCandidateDto jobCandidateDto){
        
        var candidate = await applicationContext.Candidates.FindAsync(jobCandidateDto.candidateId);
        if(candidate == null){
            throw new Exception("Invalid Candidate Id...!");
        }

        var result = await GetJobOpeningById(jobOpeningId);       

        if(result != null){
            result.jobCandidates.Add(new JobCandidate{
                candidateId = jobCandidateDto.candidateId,
                noOfInterviewRounds = jobCandidateDto.interviewRounds
            });

            await applicationContext.SaveChangesAsync();
        }
        return result;
}

public async Task<JobCandidate> AddJobCandidateByCandidate(int jobOpeningId, string candidateId)
    {
        var candidate = await applicationContext.Candidates.FindAsync(candidateId);
        if(candidate == null){
            throw new Exception("Invalid Candidate Id...!");
        }

        var jobOpening = await GetJobOpeningById(jobOpeningId); 
    
        if(jobOpening.jobStatus.status != "Open"){
            throw new Exception("Job is not open..!");
        }

        var jobCandidate = new JobCandidate{
            candidateId = candidateId,
        };
        
        jobOpening.jobCandidates.Add(jobCandidate);

        await applicationContext.SaveChangesAsync();
        
        return jobCandidate;
    }





//===============================================Read==============================================================================
    public async Task<JobOpening> GetJobOpeningById(int id)
    {
        var result =  await applicationContext.JobOpenings
                    .Include(jo=>jo.position)
                    .Include(jo=>jo.organisation)
                    .Include(jo=>jo.jobStatus)
                    .Include(jo=>jo.jobType)
                    .Include(jo=>jo.jobSkills)
                        .ThenInclude(js => js.skill)
                    .Include(jo=>jo.jobCandidates)
                        .ThenInclude(jc=>jc.candidate)
                    .FirstOrDefaultAsync(jo => jo.JobOpeningId==id);
        
        return result;
    }


public async Task<IEnumerable<JobOpening>> GetOrganisationJobOpenings(string organisationId){
    var organisation = await organisationRepository.GetOrganisationById(organisationId);
    if(organisation == null){
        throw new Exception("Organisation not found");
    }
    var result = await applicationContext.JobOpenings
                    .Include(jo=>jo.position)
                    .Include(jo=>jo.organisation)
                    .Include(jo=>jo.jobStatus)
                    .Include(jo=>jo.jobType)
                    .Include(jo=>jo.jobSkills)
                        .ThenInclude(js => js.skill)
                    .Include(jo=>jo.jobCandidates)
                        .ThenInclude(jc=>jc.candidate)
                    .Where(jo => jo.organisationId == organisationId).ToListAsync();

    return result;
}

 public async Task<IEnumerable<JobOpening>> GetCandidateJobOpenings(string candidateId){
    var candidate = await  applicationContext.Candidates.FindAsync(candidateId);
    if(candidate == null){
        throw new Exception("Candidate not found");
    }
    var result = await applicationContext.JobOpenings
                    .Include(jo=>jo.position)
                    .Include(jo=>jo.organisation)
                    .Include(jo=>jo.jobStatus)
                    .Include(jo=>jo.jobType)
                    .Include(jo=>jo.jobSkills)
                        .ThenInclude(js => js.skill)
                    .Include(jo=>jo.jobCandidates)
                         .Where(jo => jo.jobCandidates.Any(jc => jc.candidateId == candidateId)) // Filter based on candidateId
                    .ToListAsync();

    return result;
}   
    
    
    public async Task<IEnumerable<JobOpening>> GetJobOpenings()
    {
        var result = await applicationContext.JobOpenings
                    .Include(jo=>jo.position)
                    .Include(jo=>jo.organisation)
                    .Include(jo=>jo.jobStatus)
                    .Include(jo=>jo.jobType)
                    .Include(jo=>jo.jobSkills)
                        .ThenInclude(js => js.skill)
                    .Include(jo=>jo.jobCandidates)
                        .ThenInclude(jc=>jc.candidate)
                    .ToListAsync();

    return result;

    }



    public async Task<JobOpening> GetJobOpeningByName(string name)
    {
        return await applicationContext.JobOpenings.FirstOrDefaultAsync(jo=>jo.jobName.ToLower().Equals(name.ToLower()));
    }



//==============================================Update=====================================================================
    public async Task<JobOpening> UpdateJobOpeningById(int jobOpeningId, UpdateJobOpeningDto createJobOpeningDto)
    {
        //Validationg all the data respected to their Id's
        var position = await positionRepository.GetPositionById(createJobOpeningDto.positionId);
        if(position == null){
            throw new Exception("Position Not Found !");
        }
        
        var organisation = await organisationRepository.GetOrganisationById(createJobOpeningDto.organisationId);
        if(organisation == null){
            throw new Exception("Organisatoin Not Found");
        }
        
        var jobStatus = await applicationContext.JobStatuses.FindAsync(createJobOpeningDto.jobStatusId);
        if(jobStatus == null){
            throw new Exception("Invalid JobStatus Id");
        }

        var jobType = await applicationContext.JobTypes.FindAsync(createJobOpeningDto.jobTypeId);
        if(jobType == null){
            throw new Exception("Invalid JobType Id");
        }

        
        var result = await GetJobOpeningById(jobOpeningId);       
        if(result != null){

            //--------------Updating job skills----------------//    
            List<JobSkill> newJobSkills = new List<JobSkill>();
            foreach (var js in createJobOpeningDto.jobSkills)
            {
                var skill = await applicationContext.Skills.FindAsync(js.skillId);
                if (skill == null)
                {
                    throw new Exception("Invalid Skill id: " + js.skillId);

                }

                newJobSkills.Add(new JobSkill
                {
                    jobOpeningId = jobOpeningId,
                    skillId = js.skillId,
                    isRequired = js.isRequired,
                });
            }
            
            result.jobName = createJobOpeningDto.jobName;
            result.jobDescription = createJobOpeningDto.jobDescription;
            result.experienceRequired = createJobOpeningDto.experienceRequired;
            result.minSalary = createJobOpeningDto.minSalary;
            result.maxSalary = createJobOpeningDto.maxSalary;
            result.requiredCandidates = createJobOpeningDto.requiredCandidates;
            result.deadLine = createJobOpeningDto.deadLine;
            result.addtionalInfo = createJobOpeningDto.addtionalInfo;
            result.position = position;
            result.organisation = organisation;
            result.jobType = jobType;
            result.jobStatus = jobStatus;
            result.jobSkills = newJobSkills;
            await applicationContext.SaveChangesAsync();
        }
        return result;
    }




//==========================================Delete===================================================================
    public async Task<JobOpening> DeleteJobOpeningById(int id)
    {
        var result = await GetJobOpeningById(id);
        if(result != null){
            applicationContext.Remove(result);
            await applicationContext.SaveChangesAsync();
        }
        return result;
    }

    
}