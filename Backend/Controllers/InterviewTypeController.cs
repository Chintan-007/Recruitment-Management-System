using Microsoft.AspNetCore.Mvc;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;

namespace RecruitmentManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InterviewTypeController : ControllerBase
{
    private readonly IInterviewTypeRepository interviewTypeRepository;
    public InterviewTypeController(IInterviewTypeRepository interviewTypeRepository){
        this.interviewTypeRepository = interviewTypeRepository;
    }


    //Create
    [HttpPost]
    public async Task<ActionResult<InterviewType>> AddRoundHandler(InterviewType interviewType){
       try{
            if(interviewType == null){
                return BadRequest();
            }
            //check if this type is already exists or not
            var result = await interviewTypeRepository.GetInterviewTypeByType(interviewType.interviewType);
            if(result == null){
                var createdInterviewType = await interviewTypeRepository.AddInterviewType(interviewType);
                return CreatedAtAction(nameof(GetInterviewTypeById),new {id = createdInterviewType.id},createdInterviewType);
            }
            else{
                return StatusCode(StatusCodes.Status208AlreadyReported,$"The round handler type: {interviewType.interviewType} already exists!");
            }
       }
       catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Error Adding Round handler type");
       }
    }



    //Read
    [HttpGet]
    public async Task<ActionResult> GetInterviewTypes(){
        try{
            return Ok(await interviewTypeRepository.GetInterviewTypes());
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Error Reterieving data from database");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<InterviewType>> GetInterviewTypeById(int id){
        try{
            var result = await interviewTypeRepository.GetInterviewTypeById(id);
            if(result == null){
                return NotFound($"Interview type with Id: {id} not found !");
            }
            return result;
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Error Reterieving data from database");
        }
    }



    //Update
    [HttpPut("{id:int}")]
    public async Task<ActionResult<InterviewType>> UpdateInterviewTypeById(int id, InterviewType interviewType){
        try{
            var result = await interviewTypeRepository.UpdateInterviewTypeById(id,interviewType);
            if(result == null){
                return NotFound($"Interview type with Id: {id} not found !");
            }
            return result;
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Couldn't update the Interview type...!");
        }
    }


    //Delete
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<InterviewType>> DeleteInterviewTypeById(int id){
        try{
            var result = await interviewTypeRepository.DeleteInterviewTypeById(id);
            if(result == null){
                return NotFound($"Interview type with Id: {id} not found !");
            }
            return result;
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Couldn't delete the Interview type...!");
        }
    }
    
}