using Microsoft.AspNetCore.Mvc;
using SimpleBookingSystem.Models;
using SimpleBookingSystem.Services.Interfaces;
using SimpleBookingSystem.Utilities.Helpers;

namespace SimpleBookingSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResourcesController: ControllerBase
{
    private readonly IResourceService _resourceService;
    
    public ResourcesController(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetResources()
    {
        var resources = await _resourceService.GetAllAsync();
        
        var response = new ApiResponse<IEnumerable<Resource>>(false, "Data retrieved successfully.", resources);

        return Ok(response);
    }
}