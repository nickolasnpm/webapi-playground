using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi_playground.Models.DTOs.Auth;
using webapi_playground.Repository.Interface.AuthRepo;

namespace webapi_playground.Controllers.Authentication;

[ApiController]
[Route("api/[controller]")]
public class AuthReadController : ControllerBase
{
    private readonly IReadRepo _readRepo;
    public AuthReadController(IReadRepo readRepo)
    {
        _readRepo = readRepo;
    }

    [HttpGet("get-all")]
    [Authorize(Roles = "principal,teacher")]
    public async Task<IActionResult> GetAllUserAsync()
    {
        List<AuthResponse> responseList = await _readRepo.GetUserList();
        return Ok(responseList);
    }

    [HttpGet("get-by-id")]
    [Authorize]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        if (Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!) != id)
        {
            return BadRequest("You are not allowed to access this information");
        }

        AuthResponse? response = await _readRepo.GetUserById(id);

        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }    
}