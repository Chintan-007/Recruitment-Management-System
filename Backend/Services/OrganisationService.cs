using Microsoft.EntityFrameworkCore;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Services;

public class OrganisationService : IOrganisationRepository
{
    private readonly ApplicationContext applicationContext;
    public OrganisationService(ApplicationContext applicationContext){
        this.applicationContext = applicationContext;
    }

    //Create
    public async Task<Organisation> AddOrganisation(Organisation organisation)
    {
        var result = await applicationContext.Organisations.AddAsync(organisation);
        await applicationContext.SaveChangesAsync();
        return result.Entity;
    }



    //Read
    public async Task<Organisation> GetOrganisationById(string id)
    {
        return await applicationContext.Organisations.Include(org=>org.organisationType).FirstOrDefaultAsync(org => org.Id == id);
    }

    public async Task<IEnumerable<Organisation>> GetOrganisations()
    {
        return await applicationContext.Organisations.Include(org=>org.organisationType).ToListAsync();
    }

    public async Task<Organisation> GetOrganisatoinByName(string name)
    {
        return await applicationContext.Organisations.FirstOrDefaultAsync(ot => ot.UserName.ToLower().Equals(name.ToLower()));
    }



    //Update
    public async Task<Organisation> UpdateOrgranisationById(string id, Organisation organisation)
    {
        var result = await GetOrganisationById(id);
        if(result != null){
            result.UserName = organisation.UserName;
            await applicationContext.SaveChangesAsync();
        }
        return result;
    }

    public async Task<Organisation> DisableOrganisationById(string id,string reason)
    {
         var result = await GetOrganisationById(id);
        if(result != null){
            result.isActive = false;
            result.disableReason = reason;
            await applicationContext.SaveChangesAsync();
        }
        return result;
    }
    
    public async Task<Organisation> EnableOrganisationById(string id){
        var result = await GetOrganisationById(id);
        if(result != null){
            result.isActive = true;
            await applicationContext.SaveChangesAsync();
        }
        return result;
    }


    
    //Delete
    public async Task<Organisation> DeleteOrganisationById(string id){
        var result = await GetOrganisationById(id);
        if(result != null){
            applicationContext.Remove(result);
            await applicationContext.SaveChangesAsync();
        }
        return result;
    }
    

}