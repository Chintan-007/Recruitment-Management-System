using Microsoft.EntityFrameworkCore;
using RecruitmentManagement.DTOs.JobCandidates;
using RecruitmentManagement.DTOs.JobOpening;
using RecruitmentManagement.Mappers;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Service;

public class JobCandidateService : IJobCandidateRepository
{
    private readonly ApplicationContext applicationContext;
    // private readonly IPositionRepository positionRepository;
    // private readonly IOrganisationRepository organisationRepository;

    public JobCandidateService(ApplicationContext applicationContext, IPositionRepository positionRepository, IOrganisationRepository organisationRepository){
        this.applicationContext = applicationContext;
        // this.positionRepository = positionRepository;
        // this.organisationRepository = organisationRepository;
    }   

    public async Task<JobCandidate> GetJobCandidateById(int jobCandidateId)
    {
        return await applicationContext.JobCandidates.FirstOrDefaultAsync(jc => jc.id ==jobCandidateId);
    }

    public async Task<JobCandidate> UpdateJobCandidateById(int jobCandidateId, UpdateJobCandidateDto jobCandidateDto)
    {
        var existingJobCandidate = await GetJobCandidateById(jobCandidateId);
        if(existingJobCandidate == null){
            throw new Exception($"JobCanidati with id: {jobCandidateId} not found...!");
        }

        existingJobCandidate.noOfInterviewRounds = jobCandidateDto.interviewRounds;
        existingJobCandidate.isFiltered = jobCandidateDto.isFiltered;
        existingJobCandidate.isSelected = jobCandidateDto.isSelected;

        await applicationContext.SaveChangesAsync();

        return existingJobCandidate;

    }
}
