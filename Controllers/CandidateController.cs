using Microsoft.AspNetCore.Mvc;
using RecruitmentManagement.DTOs.CandidateDocs;
using RecruitmentManagement.Mappers;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CandidateController : ControllerBase
{
    private readonly ICandidateDocsRepository candidateDocsRepository;
    private readonly IDocumentTypeRepository documentTypeRepository;
    public CandidateController(ICandidateDocsRepository candidateDocsRepository,IDocumentTypeRepository documentTypeRepository){
        this.candidateDocsRepository = candidateDocsRepository;
        this.documentTypeRepository = documentTypeRepository;
    }

    [HttpPost("upload-docs")]
    public async Task<ActionResult<AddedCandidateDocsDto>> AddCandidateDocs(NewCandidateDocsDto newCandidateDocsDto){
        try{
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var updatedCandidate = await candidateDocsRepository.AddCandidateDocs(newCandidateDocsDto);
           
           //Getting document dtails for dto
            List<DisplayDocs> documentDetlails = new List<DisplayDocs>(); 
            foreach(var candidateDoc in updatedCandidate.candidateDocs){
                var doctype = await documentTypeRepository.GetDocumentTypeById(candidateDoc.documentTypeId);
                if(doctype == null){
                    throw new Exception("Document type not found");
                }
                documentDetlails.Add(new DisplayDocs{
                    documentName = doctype.documentType,
                    link = candidateDoc.documentLink
                });
                Console.WriteLine("======================================================\n+++++++++++++++++++++++++++++++++++++\nDocument added is : "+doctype.documentType);
            }
            updatedCandidate.AddedDocsModelToDto(documentDetlails);
            return Ok();
        }
        catch(Exception e){
            return StatusCode(StatusCodes.Status500InternalServerError,e);
        }
    }
}