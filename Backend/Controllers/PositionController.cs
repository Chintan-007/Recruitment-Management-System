using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagement.DTOs.Positions;
using RecruitmentManagement.Mappers;
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
    [Authorize(Roles ="Admin")]
    public async Task<ActionResult<Position>> AddPosition(NewPositionDto position){
       try{

            if(position == null){
            return BadRequest();
            }

            //Validating that position does not exists
            var result = await positionRepository.GetPositionByName(position.position);
            if(result == null){
                var createdPosition = await positionRepository.AddPosition(position.DtoToPositionModel());
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
    public async Task<ActionResult<GetPositionDto>> GetPositions(){
        try{
            var positions = await positionRepository.GetPositions();
            return Ok(positions.Select(p=>p.ModelToGetPositionDto()).ToList());
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Error Reterieving data from database");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetPositionDto>> GetPositionById(int id){
        try{
            var result = await positionRepository.GetPositionById(id);
            if(result == null){
                return NotFound($"Position with id: {id} not found");
            }
            return result.ModelToGetPositionDto();
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Error Reterieving data from database");
        }
    }



    //Update
    [HttpPut("{id:int}")]
    [Authorize(Roles ="Admin")]
    public async Task<ActionResult<GetPositionDto>> UpdatePositionById(int id, NewPositionDto position){
        try{
            var result = await positionRepository.UpdatePositionById(id,position);
            if(result == null){
                return NotFound($"Position with id: {id} not found");
            }
            return result.ModelToGetPositionDto();
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Could not update the position...!");
        }
    }


    //Delete
    [HttpDelete("{id:int}")]
    [Authorize(Roles ="Admin")]
    public async Task<ActionResult<GetPositionDto>> DeletePositionById(int id){
        try{
            var result = await positionRepository.DeletePositionById(id);
            if(result == null){
                return NotFound($"Position with id: {id} not found");
            }
            return result.ModelToGetPositionDto();
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Could not Delete the position...!");
        }
    }
}