using RecruitmentManagement.DTOs.OrganisationTypes;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Mappers;

public static class OrganisationTypeMapper
{
    public static GetOrgTypeDto ModelToGetOrgTypeDto(this OrganisationType organisationTypeModel){
        return new GetOrgTypeDto{
            id = organisationTypeModel.id,
            organisationType = organisationTypeModel.organisationType
        };
    }
}