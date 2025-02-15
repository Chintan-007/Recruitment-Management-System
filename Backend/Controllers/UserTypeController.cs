// using Microsoft.AspNetCore.Http.HttpResults;
// using Microsoft.AspNetCore.Mvc;
// using RecruitmentManagement.Models;
// using RecruitmentManagement.Repositories;

// namespace RecruitmentManagement.Controllers;

// [Route("api/[controller]")]
// [ApiController]
// public class UserTypeController : ControllerBase
// {
//     private readonly IUserTypeRepository userTypeRepository;

//     public UserTypeController(IUserTypeRepository userTypeRepository){
//         this.userTypeRepository = userTypeRepository;
//     }

// [HttpPost]
//     public async Task<ActionResult<UserType>> CreateUserType(UserType userType){
//         try{
//             if(userType == null){
//                 return BadRequest();
//             }

//             // Check if usertype already exists or not
//             var result = await userTypeRepository.GetUserTypeByTypeOfUser(userType.TypeOfUser);
//             if(result == null){
//                 var createdUserType = await userTypeRepository.AddUserType(userType);
//                 return CreatedAtAction(nameof(GetUserTypeById),new {id = createdUserType.Id},createdUserType);
//             }else{
//                 return StatusCode(StatusCodes.Status208AlreadyReported, $"The user type: {userType.TypeOfUser} already exists");
//             }

//         }catch(Exception){
//             return StatusCode(StatusCodes.Status500InternalServerError);
//         }
//     }

//     [HttpGet]
//     public async Task<ActionResult> GetUserTypes(){
//         try{
//             return Ok(await userTypeRepository.GetUserTypes());
//         }   
//         catch(Exception){
//             return StatusCode(StatusCodes.Status500InternalServerError,
//                     "Error Retrieving Data from database");
//         }
//     }

//     [HttpGet("{id:long}")]
//     public async Task<ActionResult<UserType>> GetUserTypeById(long id){
//         try{
//             var result = await userTypeRepository.GetUserTypeById(id);
//             if(result == null) return NotFound();
//             return result;
//         }
//         catch(Exception){
//             return StatusCode(StatusCodes.Status500InternalServerError,
//                         "Error Retrieving data From the database");
//         }
//     }

    

//     [HttpPut("{id:long}")]
//     public async Task<ActionResult<UserType>> UpdateUserType(long id, UserType userType){

//         try{
//             var userTypeToUpdate = await userTypeRepository.GetUserTypeById(id);
//             if(userTypeToUpdate == null){
//                 return NotFound($"UserType with Id = {id} not found");
//             }
//             return await userTypeRepository.UpdateUserType(id,userType);
//         }
//         catch(Exception){
//             return StatusCode(StatusCodes.Status500InternalServerError,
//                             "Error Updating Data");
//         }
//     }


//     [HttpDelete("{id:long}")]
//     public async Task<ActionResult<UserType>> DeleteUserType(long id){
//         try{
//             var usertypeToDelete = await userTypeRepository.GetUserTypeById(id);
//             if(usertypeToDelete == null){
//                 return NotFound($"Usertype with id: {id} not found");
//             }
//             return await userTypeRepository.DeleteUserType(id);
//         }
//         catch(Exception){
//             return StatusCode(StatusCodes.Status500InternalServerError,
//                         "Error Deleting data");
//         }
//     }
// }