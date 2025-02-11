using Microsoft.EntityFrameworkCore;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Services;

public class CandidateSkillService : ICandidateSkillRepository
{

    private readonly ApplicationContext applicationContext;
    public CandidateSkillService(ApplicationContext applicationContext){
        this.applicationContext = applicationContext;
    }

    
    public async Task AddSkillsToCandidate(string candidateId, List<int> skillIds)
    {       
        var candidate = await applicationContext.Candidates
                                  .Include(c => c.candidateSkills)
                                  .FirstOrDefaultAsync(c => c.Id == candidateId);

        // Create a list of CandidateSkill entities to add to the candidate
        var candidateSkills = skillIds.Select(skillId => new CandidateSkill
        {
            candidateId = candidateId,
            skillId = skillId
        }).ToList();

        // Add the skills to the candidate
        candidate.candidateSkills.AddRange(candidateSkills);

        // Save changes to the database
        await applicationContext.SaveChangesAsync();
    }
}