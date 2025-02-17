using RecruitmentManagement.DTOs.JobCandidates;
using RecruitmentManagement.DTOs.JobOpening;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Repositories;

public interface IJobOpeningRepository
{

    //Create
    public Task<JobOpening> CreateJobOpening(CreateJobOpeningDto createJobOpeningDto,string organisationId);
    // Read
    public Task<JobOpening> GetJobOpeningById(int id);
    public Task<IEnumerable<JobOpening>> GetOrganisationJobOpenings(string organisationId);
    Task<IEnumerable<JobOpening>> GetJobOpenings();
    Task<IEnumerable<JobOpening>> GetCandidateJobOpenings(string candidateId);
    public Task<JobOpening> GetJobOpeningByName(string name);

    // Update
    public Task<JobOpening> UpdateJobOpeningById(int id, UpdateJobOpeningDto updateJobOpeningDto);
    public Task<JobOpening> AddJobCandidateByOrganisation(int jobOpeningId,NewJobCandidateDto jobCandidateDto);
    Task<JobCandidate> AddJobCandidateByCandidate(int jobOpeningId, string candidateId);
    // public Task<JobOpening> EnableJobOpeningById(int id);

    // Delete
    public Task<JobOpening> DeleteJobOpeningById(int id);
}