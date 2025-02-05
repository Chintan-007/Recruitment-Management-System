using Microsoft.AspNetCore.Mvc;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;
using RecruitmentManagement.Services;

namespace RecruitmentManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserTypeController : ControllerBase
{
    private readonly IUserTypeRepository userTypeService;

    public UserTypeController(IUserTypeRepository userTypeService){
        this.userTypeService = userTypeService;
    }

    [HttpGet]
    public async Task<ActionResult> GetUserTypes(){
        try{
            return Ok(await userTypeService.GetUserTypes());
        }   
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Retrieving Data from database");
        }
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<UserType>> GetUserTypeById(long id){
        try{
            var result = await userTypeService.GetUserTypeById(id);
            if(result == null) return NotFound();
            return result;
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error Retrieving data From the database");
        }
    }

    [HttpPost]
    public async Task<ActionResult<UserType>> CreateUserType(UserType userType){
        try{
            if(userType == null){
                return BadRequest();
            }

            // Check if usertype already exists or not
            var result = await userTypeService.GetUserTypeByTypeOfUser(userType.TypeOfUser);
            if(result == null){
                var createdUserType = await userTypeService.AddUserType(userType);
                return CreatedAtAction(nameof(GetUserTypeById),
                                    new {id = createdUserType.Id},createdUserType);
            }else{
                return StatusCode(StatusCodes.Status208AlreadyReported, $"The user type: {userType.TypeOfUser} already exists");
            }

        }catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<UserType>> UpdateUserType(long id, UserType userType){

        try{
            var userTypeToUpdate = await userTypeService.GetUserTypeById(id);
            if(userTypeToUpdate == null){
                return NotFound($"UserType with Id = {id} not found");
            }
            return await userTypeService.UpdateUserType(id,userType);
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,
                            "Error Updating Data");
        }
    }


    [HttpDelete("{id:long}")]
    public async Task<ActionResult<UserType>> DeleteUserType(long id){
        try{
            var usertypeToDelete = await userTypeService.GetUserTypeById(id);
            if(usertypeToDelete == null){
                return NotFound($"Usertype with id: {id} not found");
            }
            return await userTypeService.DeleteUserType(id);
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,
                        "Error Deleting data");
        }
    }
}