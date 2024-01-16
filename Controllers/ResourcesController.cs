using Microsoft.AspNetCore.Mvc;
using SimpleBookingSystem.Services.Interfaces;

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

        return Ok(resources);
    }
}