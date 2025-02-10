
using RecruitmentManagement.DTOs.Organisation;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Mappers;

public static class OrganisationMapper
{
    public static OrganisationDTO OrganisationModelToDto(this Organisation organisationModel){
        return new OrganisationDTO{
            id = organisationModel.id,
            organisationName = organisationModel.organisationName,
            email = organisationModel.email,
            contact = organisationModel.contact,
            AddressLine1 = organisationModel.AddressLine1,
            AddressLine2 = organisationModel.AddressLine2,
            about = organisationModel.about,
            createdAt = organisationModel.createdAt,
            organisationType = organisationModel.organisationType,
            employees = organisationModel.employees
        };
    }

    public static Organisation OrganisationDtoToModel(this CreateOrganisationDto organisationDto){
        return new Organisation{
            organisationName = organisationDto.organisationName,
            email = organisationDto.email,
            contact = organisationDto.contact,
            AddressLine1 = organisationDto.AddressLine1,
            AddressLine2 = organisationDto.AddressLine2,
            about = organisationDto.about,
            password = organisationDto.password,
            organisationTypeId = organisationDto.organisationTypeId,
            organisationType = organisationDto.organisationType,
            employees = organisationDto.employees
        };

    }
}