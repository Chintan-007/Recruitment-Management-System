using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RecruitmentManagement.DTOs.Organisation;
using RecruitmentManagement.Mappers;
using RecruitmentManagement.Models;
using RecruitmentManagement.Repositories;
using RecruitmentManagement.Services;

namespace RecruitmentManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganisationController : ControllerBase
{

    private readonly IOrganisationRepository organisationRepository;
    public OrganisationController(IOrganisationRepository organisationRepository)
    {
        this.organisationRepository = organisationRepository;
    }

    //Create
    [HttpPost]
    public async Task<ActionResult<OrganisationDTO>> AddOrganisation(CreateOrganisationDto organisationDto)
    {
        try
        {
            if (organisationDto == null)
            {
                return BadRequest();
            }

            //Checking for duplicate names
            var result = await organisationRepository.GetOrganisatoinByName(organisationDto.organisationName);
            if (result == null)
            {
                var createdOrganisation = await organisationRepository.AddOrganisation(organisationDto.OrganisationDtoToModel());
                // return CreatedAtAction(nameof(GetOrganisationById),new{id = createdOrganisation.id},createdOrganisation);
                return createdOrganisation.OrganisationModelToDto();
            }
            else
            {
                return StatusCode(StatusCodes.Status208AlreadyReported, $"The Organisation with name '{organisationDto.organisationName}' already exists...!");
            }
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Could not add the organisation.");
        }
    }

    //Read
    [HttpGet]
    public async Task<ActionResult> GetOrganisations()
    {
        try
        {
            var organisations = await organisationRepository.GetOrganisations();
            if (organisations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "There are no organisations listed...!");
            }
            var organisationDtos = organisations.Select(org => org.OrganisationModelToDto());
            return Ok(organisationDtos);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error Reterieving data from database");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrganisationDTO>> GetOrganisationById(int id)
    {
        try
        {
            var result = await organisationRepository.GetOrganisationById(id);
            if (result == null)
            {
                return NotFound($"Organisation with Id: {id} not found !");
            }
            return result.OrganisationModelToDto();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error Reterieving data from database");
        }
    }

    //Update



    //Delete

}