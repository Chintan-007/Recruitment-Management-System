using RecruitmentManagement.DTOs.JobCandidates;
using RecruitmentManagement.DTOs.JobOpening;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Repositories;

public interface IJobOpeningRepository
{

    //Create
    public Task<JobOpening> CreateJobOpening(CreateJobOpeningDto createJobOpeningDto);
    // Read
    public Task<JobOpening> GetJobOpeningById(int id);
    public Task<IEnumerable<JobOpening>> GetJobOpenings();
    public Task<JobOpening> GetJobOpeningByName(string name);

    // Update
    public Task<JobOpening> UpdateJobOpeningById(int id, UpdateJobOpeningDto updateJobOpeningDto);
    public Task<JobOpening> AddJobCandidate(int jobOpeningId,NewJobCandidateDto jobCandidateDto);
    // public Task<JobOpening> EnableJobOpeningById(int id);

    // Delete
    public Task<JobOpening> DeleteJobOpeningById(int id);
}