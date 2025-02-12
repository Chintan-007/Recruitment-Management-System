using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagement.DTOs.Candidates;
using RecruitmentManagement.DTOs.Employees;
using RecruitmentManagement.DTOs.Organisation;
using RecruitmentManagement.DTOs.Users;
using RecruitmentManagement.Mappers;
using RecruitmentManagement.Repositories;
using RecruitmentManagement.Services;

namespace RecruitmentManagement.Models;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<Users> userManager;
    private readonly ITokenService tokenService;
    private readonly SignInManager<Users> signInManager;
    private readonly IOrganisationTypeRepository organisationTypeRepository;

    private readonly IOrganisationRepository organisationRepository;

    private readonly IPositionRepository positionRepository;
    private readonly ICandidateSkillRepository candidateSkillRepository;


    public AccountController(UserManager<Users> userManager, IOrganisationRepository organisationRepository,
                            ITokenService tokenService,SignInManager<Users> signInManager, 
                            IOrganisationTypeRepository organisationTypeRepository,IPositionRepository positionRepository,
                            ICandidateSkillRepository candidateSkillRepository){
        this.userManager = userManager;
        this.tokenService = tokenService;
        this.signInManager = signInManager;
        this.organisationTypeRepository = organisationTypeRepository;
        this.organisationRepository = organisationRepository;
        this.positionRepository = positionRepository;
        this.candidateSkillRepository = candidateSkillRepository;
    }
    


    //Registeration APIs
    [HttpPost("register-user")]
    public async Task<ActionResult> register(RegisterUserDto registerUserDto){
        try{
            if(!ModelState.IsValid){
                return BadRequest();
            }
            
            var appUser = new Users{
                UserName = registerUserDto.name,
                Email = registerUserDto.email,
                PhoneNumber = registerUserDto.mobile,
                age = registerUserDto.age,
            };

            var createdUser = await userManager.CreateAsync(appUser,registerUserDto.password);

            if(createdUser.Succeeded){
                var roleResult = await userManager.AddToRoleAsync(appUser,"Admin");
                if(roleResult.Succeeded)
                    return Ok(
                        new CreatedUserDto{
                            name = appUser.UserName,
                            email = appUser.Email,
                            token = tokenService.CreateToken(appUser)
                        }
                    );
                else
                    return StatusCode(500,roleResult.Errors);
            }
            else{
                return StatusCode(500,createdUser.Errors);
            }
        }
        catch(Exception){
            return StatusCode(StatusCodes.Status500InternalServerError,"Could not register...!");
        }
    }

    [HttpPost("register-organisation")]
    public async Task<ActionResult> RegisterOrganisation(CreateOrganisationDto createOrganisationDto){
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //Checking for valid organisation_type
            var orgType = await organisationTypeRepository.GetOrganisationTypeById(createOrganisationDto.organisationTypeId);
            if(orgType == null){
                return BadRequest("Invalid Organisation type...!");
            }

            var org = createOrganisationDto.OrganisationDtoToModel(orgType);
            var createdOrg = await userManager.CreateAsync(org,createOrganisationDto.password);
            if(createdOrg.Succeeded){
                var roleResult = await userManager.AddToRoleAsync(org,"Organisation");
                if(roleResult.Succeeded){
                    return Ok(org.OrganisationModelToDto(tokenService.CreateToken(org)));
                }
                else{
                    return StatusCode(500,roleResult.Errors);
                }
            }else{
                return StatusCode(500,createdOrg.Errors);
            }

        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not register the organisation."+e.Message);
        }
    }

    [HttpPost("register-employee")]
    public async Task<ActionResult> RegisterEmployee(CreateEmployeeDto createEmployeeDto){
        try{
        if(!ModelState.IsValid){
            return BadRequest();
        }

            // Checking for valid organisation
            var org = await organisationRepository.GetOrganisationById(createEmployeeDto.organisationId);
            if(org == null){
                return BadRequest("Invalid Organisation...!");
            }
            // Checking for valid position
            var pos = await positionRepository.GetPositionById(createEmployeeDto.positionId);
            if(pos == null){
                return BadRequest("Invalid Position...!");
            }

            var emp = createEmployeeDto.EmployeeDtoToModel(org,pos);
            var createdEmp = await userManager.CreateAsync(emp,createEmployeeDto.password);

            if(createdEmp.Succeeded){
                var roleResult = await userManager.AddToRoleAsync(emp,"Employee");
                if(roleResult.Succeeded){
                    return Ok(emp.EmployeeModelToDto(tokenService.CreateToken(emp)));
                }
                else{
                    return StatusCode(500,roleResult.Errors);
                }
            }else{
                return StatusCode(500,createdEmp.Errors);
            }

        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not register the employee."+e);
        }
    }



    [HttpPost("register-candidate")]
    public async Task<ActionResult> RegisterCandidate(CreateCandidateDto createCandidateDto){
    
         try{
        if(!ModelState.IsValid){
            return BadRequest();
        }

            // Checking for valid position
            var pos = await positionRepository.GetPositionById(createCandidateDto.positionId);
            if(pos == null){
                return BadRequest("Invalid Position...!");
            }

            var cand = createCandidateDto.CandidateDtoToModel(pos);
            // Add skills to the candidate
            await candidateSkillRepository.AddSkillsToCandidate(cand,createCandidateDto.candidateSkillsIds);

           
            var createdCand = await userManager.CreateAsync(cand,createCandidateDto.password);
            // Console.WriteLine("-----------------These are candidate Skills: ------------\n"+createCandidateDto.candidateSkillsIds[1]);
        
            if(createdCand.Succeeded){
                var roleResult = await userManager.AddToRoleAsync(cand,"Candidate");
                if(roleResult.Succeeded){
                    return Ok(cand.CandidateModelToDto(tokenService.CreateToken(cand)));
                }
                else{
                    return StatusCode(500,roleResult.Errors);
                }
            }else{
                return StatusCode(500,createdCand.Errors);
            }

        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not register the employee."+e);
        }
        
    }


    //Login APIs
    [HttpPost("login-user")]
    public async Task<ActionResult> Login(LoignDto loignDto){
        var user = await userManager.Users.FirstOrDefaultAsync(user => user.UserName == loignDto.name);
        
        if(user == null) return Unauthorized("No user found !");

        var result = await signInManager.CheckPasswordSignInAsync(user,loignDto.password,false);

        if(!result.Succeeded) return Unauthorized("Wring credentials !");

        return Ok(
            new CreatedUserDto{
                name = user.UserName,
                email = user.Email,
                token = tokenService.CreateToken(user)
            }
        );
    }
}