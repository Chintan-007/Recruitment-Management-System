using RecruitmentManagement.Models;

namespace RecruitmentManagement.Repositories;

public interface IOrganisationTypeRepository{

    // Create
    public Task<OrganisationType> AddOrganisationType(OrganisationType organisationType);

    // Read
    public Task<OrganisationType> GetOrganisationTypeById(int id);
    public Task<IEnumerable<OrganisationType>> GetOrganisationTypes();
    public Task<OrganisationType> GetOrganisatoinTypeByName(string name);

    // Update
    public Task<OrganisationType> UpdateOrgranisationTypeById(int id, OrganisationType organisationType);

    // Delete
    public Task<OrganisationType> DeleteOrganisationTypeById(int id);
}