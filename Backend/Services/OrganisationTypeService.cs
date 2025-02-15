using Microsoft.EntityFrameworkCore;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Services;

public class OrganisationTypeService : IOrganisationTypeRepository
{
    private readonly ApplicationContext applicationContext;
    public OrganisationTypeService(ApplicationContext applicationContext){
        this.applicationContext = applicationContext;
    }

    //Create
    public async Task<OrganisationType> AddOrganisationType(OrganisationType organisationType)
    {
        var result = await applicationContext.OrganisationTypes.AddAsync(organisationType);
        await applicationContext.SaveChangesAsync();
        return result.Entity;
    }



    //Read
    public async Task<OrganisationType> GetOrganisationTypeById(int id)
    {
        return await applicationContext.OrganisationTypes.FindAsync(id);
    }

    public async Task<IEnumerable<OrganisationType>> GetOrganisationTypes()
    {
        return await applicationContext.OrganisationTypes.ToListAsync();
    }

    public async Task<OrganisationType> GetOrganisatoinTypeByName(string name)
    {
        return await applicationContext.OrganisationTypes.FirstOrDefaultAsync(ot => ot.organisationType.ToLower().Equals(name.ToLower()));
    }



    //Update
    public async Task<OrganisationType> UpdateOrgranisationTypeById(int id, OrganisationType organisationType)
    {
        var result = await GetOrganisationTypeById(id);
        if(result != null){
            result.organisationType = organisationType.organisationType;
            await applicationContext.SaveChangesAsync();
        }
        return result;
    }


    
    //Delete
    public async Task<OrganisationType> DeleteOrganisationTypeById(int id)
    {
         var result = await GetOrganisationTypeById(id);
        if(result != null){
            applicationContext.Remove(result);
            await applicationContext.SaveChangesAsync();
        }
        return result;
    }

}