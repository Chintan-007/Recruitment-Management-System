using RecruitmentManagement.DTOs.JobCandidates;
using RecruitmentManagement.DTOs.JobOpening;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Mappers;

public static class JobCandidateMapper{

    public static AddedJobCandidateDto ModelToAddedJobCandidateDto(this JobOpening jobOpeningModel){
            return new AddedJobCandidateDto{
                jobName = jobOpeningModel.jobName,
                jobDescription= jobOpeningModel.jobDescription,
                position = jobOpeningModel.position.position,
                organisation = jobOpeningModel.organisation.firstName+jobOpeningModel.organisation.lastName,
                jobType = jobOpeningModel.jobType.type,
                jobStatus = jobOpeningModel.jobStatus.status,
                candidates = jobOpeningModel.jobCandidates.Select(jc=>new CandidateInfoDto{
                                                                            candidateId = jc.candidate.Id,
                                                                            candidateUserName=jc.candidate.UserName,
                                                                            interviewRounds = jc.noOfInterviewRounds,
                                                                            isFiltered = jc.isFiltered,
                                                                            isSelected = jc.isSelected
                                                                        }
                ).ToList()
            };  
    }

    public static CandidateInfoDto ModelToGetCanidateForOrganisation(this JobCandidate jobCandidateModel){
            return new CandidateInfoDto{
                candidateId = jobCandidateModel.candidateId,
                candidateUserName = jobCandidateModel.candidate.UserName,
                interviewRounds = jobCandidateModel.noOfInterviewRounds,
                isFiltered = jobCandidateModel.isFiltered,
                isSelected = jobCandidateModel.isSelected
            };  
    }
    public static AfterUpdateJobCandidateDto ModelToUpdatedJobCandidateDto(this JobCandidate jobCandidateModel){
            return new AfterUpdateJobCandidateDto{
                candidateId = jobCandidateModel.candidateId,
                jobId = jobCandidateModel.jobOpeningId,
                interviewRounds = jobCandidateModel.noOfInterviewRounds,
                isFiltered = jobCandidateModel.isFiltered,
                isSelected = jobCandidateModel.isSelected
            };  
    }

}