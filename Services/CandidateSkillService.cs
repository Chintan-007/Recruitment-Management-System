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

    
    public async Task AddSkillsToCandidate(Candidate cand, List<int> skillIds)
    {       

        try{
       
        foreach (int skillId in skillIds)
        {   
            Console.WriteLine("=======The skill Id is: "+skillId);
            var skill = await applicationContext.Skills.FindAsync(skillId);
            if(skill == null) Console.WriteLine("Skill we got is: null");
            if (skill != null && cand !=null)
            {
                cand.candidateSkills.Add(new CandidateSkill
                {
                    candidateId = cand.Id,
                    skillId = skillId
                });
                Console.WriteLine("==========Skill Added Id: "+skillId);
            }
        }
    
        // Save changes to the database
        // await applicationContext.SaveChangesAsync();
        }
        catch(Exception){
            
        }
    }
}