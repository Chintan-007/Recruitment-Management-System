using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagement.DTOs.CandidateDocs;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Services;

public class CandidateDocsService : ICandidateDocsRepository
{
    private readonly ApplicationContext applicationContext;
    public CandidateDocsService(ApplicationContext applicationContext){
        this.applicationContext = applicationContext;
    }
    public async Task<Candidate> AddCandidateDocs(NewCandidateDocsDto newCandidateDocsDto)
    {
        //Validating the data
        var candidate = await applicationContext.Candidates.Include(c=>c.candidateDocs).FirstOrDefaultAsync(c=>c.Id == newCandidateDocsDto.candidateId);
        if(candidate == null){
            throw new Exception("Candidate not found...!");
        }

        if(newCandidateDocsDto.documentDatas == null){
            throw new Exception("Bad request");
        }

        List<CandidateDocs> addedCandidateDocs = new List<CandidateDocs>();
        foreach(var candidateDoc in newCandidateDocsDto.documentDatas){
            var documentType = await applicationContext.DocumentTypes.FindAsync(candidateDoc.documentTypeId);
            if(documentType == null){
                throw new Exception("Document type not found");
            }
            candidate.candidateDocs.Add(new CandidateDocs{
                candidateId = newCandidateDocsDto.candidateId,
                documentTypeId = candidateDoc.documentTypeId,
                documentLink = candidateDoc.documentLink
            });
        }
        await applicationContext.SaveChangesAsync();
        return candidate;
        throw new NotImplementedException();
    }


    public Task<ActionResult> GetCandidateDocs()
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<CandidateDocs>> GetCandidateDocsById(int candidateDoccId)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<CandidateDocs>> UpdateCandidateDocsById(int candidateDocsId, CandidateDocs candidateDocs)
    {
        throw new NotImplementedException();
    }


     public Task<ActionResult> DeleteCandidateDocsById(int candidateDocsId)
    {
        throw new NotImplementedException();
    }
}