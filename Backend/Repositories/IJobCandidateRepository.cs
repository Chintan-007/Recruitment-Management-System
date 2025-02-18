using RecruitmentManagement.DTOs.JobCandidates;
using RecruitmentManagement.DTOs.JobOpening;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Repositories;

public interface IJobCandidateRepository
{

    // //Create
    // public Task<JobOpening> CreateJobOpening(CreateJobOpeningDto createJobOpeningDto);
    // Read
    public Task<JobCandidate> GetJobCandidateById(int id);
    Task<JobCandidate> GetJobCandidateByjobOpeningIdAndcanidateId(int jobOpeningId, string candidateId);
    Task<IEnumerable<JobCandidate>> GetJobCanidatesByJobOpeningId(int organisationId);
    Task<JobCandidate> GetSelectedJobCandidateByCandidateId(string candidateId);

    // public Task<IEnumerable<JobOpening>> GetJobOpenings();
    // public Task<JobOpening> GetJobOpeningByName(string name);

    // Update
    public Task<JobCandidate> UpdateJobCandidateById(int id, UpdateJobCandidateDto jobCandidateDto);
    // public Task<JobOpening> AddJobCandidate(int jobOpeningId,NewJobCandidateDto jobCandidateDto);
    // public Task<JobOpening> EnableJobOpeningById(int id);

    // Delete
    // public Task<JobOpening> DeleteJobOpeningById(int id);
}