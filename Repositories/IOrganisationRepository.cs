using RecruitmentManagement.Models;

namespace RecruitmentManagement.Repositories;

public interface IOrganisationRepository{
     // Create
    public Task<Organisation> AddOrganisation(Organisation organisation);

    // Read
    public Task<Organisation> GetOrganisationById(int id);
    public Task<IEnumerable<Organisation>> GetOrganisations();
    public Task<Organisation> GetOrganisatoinByName(string name);

    // Update
    public Task<Organisation> UpdateOrgranisationById(int id, Organisation organisation);
    public Task<Organisation> DisableOrganisationById(int id, string reason);
    public Task<Organisation> EnableOrganisationById(int id);

    // Delete
    public Task<Organisation> DeleteOrganisationById(int id);
}