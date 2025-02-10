
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RecruitmentManagement.Models;

namespace RecruitmentManagement.DTOs.Users;

public class CreatedUserDto
{

    public string name{get;set;} = string.Empty;

    public string email{get;set;} = string.Empty;

    public string token{get;set;} =string.Empty;
}