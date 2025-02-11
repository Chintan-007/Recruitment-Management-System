using RecruitmentManagement.Models;

namespace RecruitmentManagement.Repositories;

public interface IOrganisationRepository{

    // Read
    public Task<Organisation> GetOrganisationById(string id);
    public Task<IEnumerable<Organisation>> GetOrganisations();
    public Task<Organisation> GetOrganisatoinByName(string name);

    // Update
    public Task<Organisation> UpdateOrgranisationById(string id, Organisation organisation);
    public Task<Organisation> DisableOrganisationById(string id, string reason);
    public Task<Organisation> EnableOrganisationById(string id);

    // Delete
    public Task<Organisation> DeleteOrganisationById(string id);
}