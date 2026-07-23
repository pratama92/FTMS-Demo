using FTMS.API.Dto.Person.Request;
using FTMS.Application.UseCases.PersonManagement.AddDriverRole;
using FTMS.Application.UseCases.PersonManagement.CreatePerson;
using FTMS.Application.UseCases.PersonManagement.DeletePerson;
using FTMS.Application.UseCases.PersonManagement.GetPersonById;
using FTMS.Application.UseCases.PersonManagement.GetPersons;
using FTMS.Application.UseCases.PersonManagement.RemoveDriverRole;
using FTMS.Application.UseCases.PersonManagement.UpdatePerson;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FTMS.API.Controllers;

[ApiController]
[Route("api/persons")]
[Authorize(Roles = "Admin,Dispatcher")]
public class PersonController : ControllerBase
{
    private readonly CreatePersonUseCase _createPersonUseCase;
    private readonly GetPersonByIdUseCase _getPersonByIdUseCase;
    private readonly GetPersonsUseCase _getPersonsUseCase;
    private readonly UpdatePersonUseCase _updatePersonUseCase;
    private readonly DeletePersonUseCase _deletePersonUseCase;
    private readonly AddDriverRoleUseCase _addDriverRoleUseCase;
    private readonly RemoveDriverRoleUseCase _removeDriverRoleUseCase;

    public PersonController(CreatePersonUseCase createPersonUseCase, GetPersonByIdUseCase getPersonByIdUseCase, GetPersonsUseCase getPersonsUseCase, UpdatePersonUseCase updatePersonUseCase, DeletePersonUseCase deletePersonUseCase, AddDriverRoleUseCase addDriverRoleUseCase, RemoveDriverRoleUseCase removeDriverRoleUseCase)
    {
        _createPersonUseCase = createPersonUseCase;
        _getPersonByIdUseCase = getPersonByIdUseCase;
        _getPersonsUseCase = getPersonsUseCase;
        _updatePersonUseCase = updatePersonUseCase;
        _deletePersonUseCase = deletePersonUseCase;
        _addDriverRoleUseCase = addDriverRoleUseCase;
        _removeDriverRoleUseCase = removeDriverRoleUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonRequest request)
    {
        var command = new CreatePersonCommand()
        {
            OrganizationId = request.OrganizationId,
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
        };

        var result = await _createPersonUseCase.ExecuteAsync(command);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool? isDeleted = null)
    {
        var command = await _getPersonsUseCase.ExecuteAsync(isDeleted);

        return Ok(command);
    }

    [HttpGet("{personId}")]
    public async Task<IActionResult> GetById([FromRoute] Guid personId)
    {
        var command = await _getPersonByIdUseCase.ExecuteAsync(personId);

        return Ok(command);
    }

    [HttpPut("{personId}")]
    public async Task<IActionResult> Update([FromRoute] Guid personId, [FromBody] UpdatePersonRequest request)
    {
        var command = new UpdatePersonCommand()
        {
            PersonId = personId,
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
        };

        var result = await _updatePersonUseCase.ExecuteAsync(command);
        return Ok(result);
    }

    [HttpPatch("{personId}/adddriverrole")]
    public async Task<IActionResult> AddDriverRole([FromRoute] Guid personId)
    {
        var result = await _addDriverRoleUseCase.ExecuteAsync(personId);
        return Ok(result);
    }

    [HttpPatch("{personId}/removedriverrole")]
    public async Task<IActionResult> RemoveDriverRole([FromRoute] Guid personId)
    {
        var result = await _removeDriverRoleUseCase.ExecuteAsync(personId);
        return Ok(result);
    }

    [HttpDelete("{personId}")]
    public async Task<IActionResult> Delete([FromRoute] Guid personId)
    {
        var result = await _deletePersonUseCase.ExecuteAsync(personId);
        return Ok(result);
    }

}