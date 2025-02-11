
using RecruitmentManagement.DTOs.Organisation;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Mappers;

public static class OrganisationMapper
{
    public static RegisteredOrganisationDto OrganisationModelToDto(this Organisation organisationModel, string orgToken){
        return new RegisteredOrganisationDto{
            firstName = organisationModel.firstName,
            lastName = organisationModel.lastName,
            userName = organisationModel.UserName,
            email = organisationModel.Email,
            contact = organisationModel.PhoneNumber,
            AddressLine1 = organisationModel.AddressLine1,
            AddressLine2 = organisationModel.AddressLine2,
            about = organisationModel.about,
            token = orgToken,
            organisationType = organisationModel.organisationType.organisationType,
            employees = organisationModel.employees
        };
    }

    public static Organisation OrganisationDtoToModel(this CreateOrganisationDto organisationDto, OrganisationType orgType){
        return new Organisation{
                firstName = organisationDto.firstName,
                lastName = organisationDto.lastName,
                UserName = organisationDto.userName,
                Email = organisationDto.email,
                PhoneNumber = organisationDto.contact,
                AddressLine1 = organisationDto.AddressLine1,
                AddressLine2 = organisationDto.AddressLine2,
                about = organisationDto.about,
                organisationType = orgType,
        };

    }
}