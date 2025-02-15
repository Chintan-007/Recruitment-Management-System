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
    Task<ActionResult<CandidateDocs>> GetCandidateDocsById(int candidateDoccId);

    //Update
    Task<ActionResult<CandidateDocs>> UpdateCandidateDocsById(int candidateDocsId, CandidateDocs candidateDocs);

    //Delete
    Task<ActionResult> DeleteCandidateDocsById(int candidateDocsId);
}