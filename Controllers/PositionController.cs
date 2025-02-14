using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;
using RecruitmentManagement.Services;

namespace RecruitmentManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PositionController : ControllerBase{
    private readonly IPositionRepository positionRepository;
    public PositionController(IPositionRepository positionRepository){
        this.positionRepository = positionRepository;
    }


    //Create
    [HttpPost]
    [Authorize(Roles ="Organisation")]
    public async Task<ActionResult<Position>> AddPosition(Position position){
       try{

            if(position == null){
            return BadRequest();
            }

            //Validating that position does not exists
            var result = await positionRepository.GetPositionByName(position.position);
            if(result == null){
                var createdPosition = await positionRepository.AddPosition(position);
                return CreatedAtAction(nameof(GetPositionById),new {id = createdPosition.id},createdPosition);
            }
            else{
                return StatusCode(StatusCodes.Status208AlreadyReported,$"The position '{position.position}' already exists");
            }

       }catch(Exception){
        return StatusCode(StatusCodes.Status500InternalServerError,"Error Adding Position...!");
       }
        
    }


    //Read
    [HttpGet]
    public async Task<ActionResult> GetPositions(){
        try{
            return Ok(await positionRepository.GetPositions());
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Error Reterieving data from database");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Position>> GetPositionById(int id){
        try{
            var result = await positionRepository.GetPositionById(id);
            if(result == null){
                return NotFound($"Position with id: {id} not found");
            }
            return result;
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Error Reterieving data from database");
        }
    }



    //Update
    [HttpPut("{id:int}")]
    public async Task<ActionResult<Position>> UpdatePositionById(int id, Position position){
        try{
            var result = await positionRepository.UpdatePositionById(id,position);
            if(result == null){
                return NotFound($"Position with id: {id} not found");
            }
            return result;
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Could not update the position...!");
        }
    }


    //Delete
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Position>> DeletePositionById(int id){
        try{
            var result = await positionRepository.DeletePositionById(id);
            if(result == null){
                return NotFound($"Position with id: {id} not found");
            }
            return result;
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Could not Delete the position...!");
        }
    }
}