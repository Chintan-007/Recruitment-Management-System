using RecruitmentManagement.DTOs.JobOpening;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Mappers;

public static class JopOpeningMapper{

    public static AddedJobOpeningDto ModelToAddedJobOpeningDto(this JobOpening jobOpeningModel){
            return new AddedJobOpeningDto{
                jobName = jobOpeningModel.jobName,
                jobDescription= jobOpeningModel.jobDescription,
                experienceRequired = jobOpeningModel.experienceRequired,
                minSalary = jobOpeningModel.minSalary,
                maxSalary = jobOpeningModel.maxSalary,
                requiredCandidates = jobOpeningModel.requiredCandidates,
                deadLine = jobOpeningModel.deadLine,
                position = jobOpeningModel.position.position,
                organisation = jobOpeningModel.organisation.firstName+jobOpeningModel.organisation.lastName,
                jobType = jobOpeningModel.jobType.type,
                jobStatus = jobOpeningModel.jobStatus.status,
                jobSkills = jobOpeningModel.jobSkills.Select(js => js.skill.skillName).ToList()
            };
      
    }

    public static GetJobOpeningDto ModelToGetJobOpeningDto(this JobOpening jobOpeningModel){
        return new GetJobOpeningDto{
            jobName = jobOpeningModel.jobName,
            jobDescription= jobOpeningModel.jobDescription,
            experienceRequired = jobOpeningModel.experienceRequired,
            minSalary = jobOpeningModel.minSalary,
            maxSalary = jobOpeningModel.maxSalary,
            requiredCandidates = jobOpeningModel.requiredCandidates,
            deadLine = jobOpeningModel.deadLine,
            position = jobOpeningModel.position.position,
            organisation = jobOpeningModel.organisation.firstName+jobOpeningModel.organisation.lastName,
            jobType = jobOpeningModel.jobType.type,
            jobStatus = jobOpeningModel.jobStatus.status,
            jobSkills = jobOpeningModel.jobSkills
        };
    }

    public static JobOpening DtoToModel(this CreateJobOpeningDto jobOpeningDto,Position position, Organisation organisation, JobType jobType, JobStatus jobStatus){
        return new JobOpening{
            jobName = jobOpeningDto.jobName,
            jobDescription= jobOpeningDto.jobDescription,
            experienceRequired = jobOpeningDto.experienceRequired,
            minSalary = jobOpeningDto.minSalary,
            maxSalary = jobOpeningDto.maxSalary,
            requiredCandidates = jobOpeningDto.requiredCandidates,
            deadLine = jobOpeningDto.deadLine,
            // positionId = jobOpeningDto.positionId,
            position = position,
            // organisationId = jobOpeningDto.organisationId,
            organisation = organisation,
            // jobTypeId = jobOpeningDto.jobTypeId,
            jobType = jobType,
            jobStatus = jobStatus,
        };
    }

}