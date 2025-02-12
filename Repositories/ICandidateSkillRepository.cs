using RecruitmentManagement.Models;

namespace RecruitmentManagement.Repositories;

public interface ICandidateSkillRepository{
    public Task AddSkillsToCandidate(Candidate cand, List<int> skillId);
}