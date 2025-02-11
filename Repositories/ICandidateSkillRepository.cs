using RecruitmentManagement.Models;

namespace RecruitmentManagement.Repositories;

public interface ICandidateSkillRepository{
    public Task AddSkillsToCandidate(string candidateId, List<int> skillId);
}