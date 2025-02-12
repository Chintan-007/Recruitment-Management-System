
using RecruitmentManagement.DTOs.Candidates;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Mappers;

public static class CandidateMapper
{
    public static RegisteredCandidateDto CandidateModelToDto(this Candidate candidateModel, string canToken){
        var skills =candidateModel.candidateSkills.Select(cs => cs.skill.skillName);
        return new RegisteredCandidateDto{
            firstName = candidateModel.firstName,
            lastName = candidateModel.lastName,
            userName = candidateModel.UserName,
            email = candidateModel.Email,
            phoneNumber = candidateModel.PhoneNumber,
            age = candidateModel.age,
            token = canToken,
            organisationName = candidateModel.organisationName,
            position = candidateModel.position.position,
            yearsOfExperience = candidateModel.yearsOfExperience,
            resumeLink = candidateModel.resumeLink,
            candidateSkills = skills
        };
    }

    public static Candidate CandidateDtoToModel(this CreateCandidateDto candidateDto, Position position){
        return new Candidate{
                firstName = candidateDto.firstName,
                lastName = candidateDto.lastName,
                UserName = candidateDto.userName,
                Email = candidateDto.email,
                PhoneNumber = candidateDto.phoneNumber,
                age = candidateDto.age,
                position = position,
                organisationName = candidateDto.organisationName,
                yearsOfExperience = candidateDto.yearsOfExperience,
                resumeLink = candidateDto.resumeLink
        };

    }
}