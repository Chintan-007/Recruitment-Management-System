using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruitmentManagement.DTOs.Users;
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
    public AccountController(UserManager<Users> userManager,ITokenService tokenService,SignInManager<Users> signInManager){
        this.userManager = userManager;
        this.tokenService = tokenService;
        this.signInManager = signInManager;
    }
    
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

    [HttpPost("login")]
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