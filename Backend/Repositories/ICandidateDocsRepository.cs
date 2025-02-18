using Microsoft.AspNetCore.Mvc;
using RecruitmentManagement.DTOs.CandidateDocs;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.Repositories;

public interface ICandidateDocsRepository
{
    //Create
    Task<Candidate> AddCandidateDocs(NewCandidateDocsDto newCandidateDocsDto);

    //Read
    Task<ActionResult> GetCandidateDocs();
   Task<IEnumerable<CandidateDocs>> GetCandidateDocsByCandidateId(string candidateId);
    Task<CandidateDocs> GetCandidateDocByCandidateIdandDocId(string candidateId, int documentTypeId);

    //Update
    Task<ActionResult<CandidateDocs>> UpdateCandidateDocsById(int candidateDocsId, CandidateDocs candidateDocs);

    //Delete
    Task<ActionResult> DeleteCandidateDocsById(int candidateDocsId);
}