using FTMS.Application.UseCases.OrganizationManagement.ActivateOrganization;
using FTMS.Application.UseCases.OrganizationManagement.CreateOrganization;
using FTMS.Application.UseCases.OrganizationManagement.DeactivateOrganization;
using FTMS.Application.UseCases.OrganizationManagement.GetOrganizationById;
using FTMS.Application.UseCases.OrganizationManagement.GetOrganizations;
using FTMS.Application.UseCases.OrganizationManagement.RenameOrganization;
using FTMS.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FTMS.API.Controllers
{
    [ApiController]
    [Route("api/organizations")]
    [Authorize(Roles = "Admin")]
    public class OrganizationController : ControllerBase
    {
        private readonly CreateOrganizationUseCase _createOrganizationUseCase;
        private readonly DeactivateOrganizationUseCase _deactivateOrganizationUseCase;
        private readonly ActivateOrganizationUseCase _activateOrganizationUseCase;
        private readonly RenameOrganizationUseCase _renameOrganizationUseCase;
        private readonly GetOrganizationByIdUseCase _getOrganizationByIdUseCase;
        private readonly GetOrganizationsUseCase _getOrganizationsUseCase;

        public OrganizationController(CreateOrganizationUseCase createOrganizationUseCase, DeactivateOrganizationUseCase deactivateOrganizationUseCase, ActivateOrganizationUseCase activateOrganizationUseCase, RenameOrganizationUseCase renameOrganizationUseCase, GetOrganizationByIdUseCase getOrganizationByIdUseCase, GetOrganizationsUseCase getOrganizationsUseCase)
        {
            _createOrganizationUseCase = createOrganizationUseCase;
            _deactivateOrganizationUseCase = deactivateOrganizationUseCase;
            _activateOrganizationUseCase = activateOrganizationUseCase;
            _renameOrganizationUseCase = renameOrganizationUseCase;
            _getOrganizationByIdUseCase = getOrganizationByIdUseCase;
            _getOrganizationsUseCase = getOrganizationsUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrganizationRequest request)
        {
            var result = await _createOrganizationUseCase.ExecuteAsync(request);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] OrganizationStatusEnum? status = null)
        {
            var result = await _getOrganizationsUseCase.ExecuteAsync(status);
            return Ok(result);
        }

        [HttpGet("{organizationId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid organizationId)
        {
            var result = await _getOrganizationByIdUseCase.ExecuteAsync(organizationId);
            return Ok(result);
        }

        [HttpPut("{organizationId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid organizationId, [FromBody] RenameOrganizationRequest request)
        {
            request.OrganizationId = organizationId;
            var result = await _renameOrganizationUseCase.ExecuteAsync(request);
            return Ok(result);
        }

        [HttpPatch("{organizationId:guid}/activate")]
        public async Task<IActionResult> Activate([FromRoute] Guid organizationId)
        {
            var result = await _activateOrganizationUseCase.ExecuteAsync(organizationId);
            return Ok(result);
        }

        [HttpPatch("{organizationId:guid}/deactivate")]
        public async Task<IActionResult> Deactivate([FromRoute] Guid organizationId)
        {
            var result = await _deactivateOrganizationUseCase.ExecuteAsync(organizationId);
            return Ok(result);
        }
    }
}
