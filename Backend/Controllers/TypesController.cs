
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagement.DTOs.DocumentTypes;
using RecruitmentManagement.Mappers;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TypeController : ControllerBase 
{
    private readonly IDocumentTypeRepository documentTypeRepository;
    public TypeController(IDocumentTypeRepository documentTypeRepository){
        this.documentTypeRepository = documentTypeRepository;
    }


    //Create
    [HttpPost("document-type")]
    [Authorize(Roles ="Admin")]
    public async Task<ActionResult<GetDocTypeDto>> AddDocumentType(NewDocTypeDto docTypeDto){
        try{
            if(docTypeDto == null){
                return BadRequest();
            }
            //check if doctype already exists or not
            var result = await documentTypeRepository.GetDocumentTypeByDocumentType(docTypeDto.documentType);
            if(result == null){
                var createdDocType = await documentTypeRepository.AddDocumentType(docTypeDto.DtoToDocTypeModel());
                return CreatedAtAction(nameof(GetDocumentTypeById),new {id = createdDocType.id},createdDocType);
            }
            else{
                return StatusCode(StatusCodes.Status208AlreadyReported,$"The doctype: {docTypeDto.documentType} already exists!");
            }
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Couldn't add the document...!");
        }
    }


    //Read
    [HttpGet("document-types")]
    [Authorize(Roles ="Admin,Candidate,Organisation")]
    public async Task<ActionResult> GetDocumentTypes(){
        try{
            var docTypes = await documentTypeRepository.GetDocumentTypes();
            return Ok(docTypes.Select(dc => dc.ModelToGetDocTypeDto()));
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Error Retrieving data from database!!!");
        }
    }

    
    [HttpGet("doucument-type/{id:int}")]
    public async Task<ActionResult> GetDocumentTypeById(int id){
        try{
            var result = await documentTypeRepository.GetDocumentTypeById(id);
            if(result == null){
                return NotFound($"Document type with Id: {id} not found !");
            }
            return Ok(result.ModelToGetDocTypeDto());
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Error Retrieving data from database");
        }
    }

    [HttpGet("organisation-types")]
    public async Task<ActionResult> GetOrganisationTypes(){
        try{
            var orgTypes = await documentTypeRepository.GetOrganisationTypes();
            return Ok(orgTypes.Select(ot => ot.ModelToGetOrgTypeDto()));
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Error Retrieving data from database!!!");
        }
    }


    //Update
    [HttpPut("document-type/{id:int}")]
    [Authorize(Roles ="Admin")]
    public async Task<ActionResult<GetDocTypeDto>> UpdateDocumentById(int id, DocumentType documentType){
        try{
            var result = await documentTypeRepository.GetDocumentTypeById(id);
            if(result == null){
                return NotFound($"Document type with Id: {id} not found !");
            }
            var docType = await documentTypeRepository.UpdateDocumentTypeById(id,documentType);
            return docType.ModelToGetDocTypeDto();
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Couldn't update the document type...!");
        }
    }


    //Delete
    [HttpDelete("document-type/{id:int}")]
    [Authorize(Roles ="Admin")]
    public async Task<ActionResult<GetDocTypeDto>> DeleteDocumentById(int id){
        try{
            var result = documentTypeRepository.GetDocumentTypeById(id);
            if(result == null){
                return NotFound($"Document type with id: {id} not found.");
            }
            var deletedDocType = await documentTypeRepository.DeleteDocumentTypeById(id);
            return deletedDocType.ModelToGetDocTypeDto();
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Couldn't delete the document type...!");
        }
    }
    
}