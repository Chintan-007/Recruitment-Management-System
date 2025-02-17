
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentTypeController : ControllerBase 
{
    private readonly IDocumentTypeRepository documentTypeRepository;
    public DocumentTypeController(IDocumentTypeRepository documentTypeRepository){
        this.documentTypeRepository = documentTypeRepository;
    }


    //Create
    [HttpPost]
    [Authorize(Roles ="Admin")]
    [Authorize(Roles ="Organisation")]
    public async Task<ActionResult<DocumentType>> AddDocumentType(DocumentType documentType){
        try{
            if(documentType == null){
                return BadRequest();
            }
            //check if doctype already exists or not
            var result = await documentTypeRepository.GetDocumentTypeByDocumentType(documentType.documentType);
            if(result == null){
                var createdDocType = await documentTypeRepository.AddDocumentType(documentType);
                return CreatedAtAction(nameof(GetDocumentTypeById),new {id = createdDocType.id},createdDocType);
            }
            else{
                return StatusCode(StatusCodes.Status208AlreadyReported,$"The doctype: {documentType.documentType} already exists!");
            }
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Couldn't add the document...!");
        }
    }


    //Read
    [HttpGet]
    [Authorize(Roles ="Admin,Candidate,Organisation")]
    public async Task<ActionResult> GetDocumentTypes(){
        try{
            return Ok(await documentTypeRepository.GetDocumentTypes());
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Error Retrieving data from database!!!");
        }
    }

    
    [HttpGet("{id:int}")]
    [Authorize(Roles ="Admin,Candidate,Organisation")]
    public async Task<ActionResult<DocumentType>> GetDocumentTypeById(int id){
        try{
            var result = await documentTypeRepository.GetDocumentTypeById(id);
            if(result == null){
                return NotFound($"Document type with Id: {id} not found !");
            }
            return result;
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Error Retrieving data from database");
        }
    }


    //Update
    [HttpPut("{id:int}")]
    [Authorize(Roles ="Admin")]
    public async Task<ActionResult<DocumentType>> UpdateDocumentById(int id, DocumentType documentType){
        try{
            var result = await documentTypeRepository.GetDocumentTypeById(id);
            if(result == null){
                return NotFound($"Document type with Id: {id} not found !");
            }
            return await documentTypeRepository.UpdateDocumentTypeById(id,documentType);
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Couldn't update the document type...!");
        }
    }


    //Delete
    [HttpDelete("{id:int}")]
    [Authorize(Roles ="Admin")]
    public async Task<ActionResult<DocumentType>> DeleteDocumentById(int id){
        try{
            var result = documentTypeRepository.GetDocumentTypeById(id);
            if(result == null){
                return NotFound($"Document type with id: {id} not found.");
            }
            return await documentTypeRepository.DeleteDocumentTypeById(id);
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Couldn't delete the document type...!");
        }
    }
    
}