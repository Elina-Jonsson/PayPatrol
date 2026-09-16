using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayPatrol.Application.Interfaces;
using System.Security.Claims;

namespace PayPatrol.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceCatalogController : ControllerBase
    {
        private readonly IServiceCatalogService _serviceCatalogService;

        public ServiceCatalogController (IServiceCatalogService serviceCatalogService)
        {
           _serviceCatalogService = serviceCatalogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
            {
                return Unauthorized();
            }

            var result = await _serviceCatalogService.GetAllServiceCatalogsAsync();

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
            {
                return Unauthorized();
            }

            var result = await _serviceCatalogService.GetServiceCatalogByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetCatalogByCategory(int categoryId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            var result = await _serviceCatalogService.GetServiceCatalogsByCategoryIdAsync(categoryId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}
