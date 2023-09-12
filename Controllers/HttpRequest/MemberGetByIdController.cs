using Microsoft.AspNetCore.Mvc;
using webapi_playground.Models.Domain.Member;
using webapi_playground.Models.DTOs.Member;
using webapi_playground.Repository.Interface.MemberRepo;

namespace webapi_playground.Controllers.HttpRequest;

[ApiController]
[Route("api/[controller]")]
public class MemberGetByIdController : ControllerBase
{

    private readonly IMemberRepo _repo;

    public MemberGetByIdController(IMemberRepo repo)
    {
        _repo = repo;
    }

    [HttpGet("from-query")]
    public async Task<IActionResult> GetFromQuery([FromQuery] Guid id)
    {
        // FromQuery normally used to retrieve single data 

        MemberDomain member = await _repo.GetAsync(id);

        if (member == null)
        {
            return NotFound();
        }

        MemberDto memberDto = new()
        {
            Name = member.Name,
            Address = member.Address,
            FromAttribute = member.FromAttribute,
        };

        return Ok(memberDto);
    }

    [HttpGet("from-header")]
    public async Task<IActionResult> GetFromHeader([FromHeader] Guid id)
    {
        // FromHeader normally used for authorization

        MemberDomain member = await _repo.GetAsync(id);

        if (member == null)
        {
            return NotFound();
        }

        MemberDto memberDto = new()
        {
            Name = member.Name,
            Address = member.Address,
            FromAttribute = member.FromAttribute,
        };

        return Ok(memberDto);
    }

    [HttpGet("from-form")]
    public async Task<IActionResult> GetFromForm([FromForm] Guid id)
    {
        // FromHeader normally used for authorization

        MemberDomain member = await _repo.GetAsync(id);

        if (member == null)
        {
            return NotFound();
        }

        MemberDto memberDto = new()
        {
            Name = member.Name,
            Address = member.Address,
            FromAttribute = member.FromAttribute,
        };

        return Ok(memberDto);
    }

    [HttpGet("from-route")]
    public async Task<IActionResult> GetFromRoute([FromRoute] Guid id)
    {
        // FromHeader normally used for authorization

        MemberDomain member = await _repo.GetAsync(id);

        if (member == null)
        {
            return NotFound();
        }

        MemberDto memberDto = new()
        {
            Name = member.Name,
            Address = member.Address,
            FromAttribute = member.FromAttribute,
        };

        return Ok(memberDto);
    }


    [HttpGet("from-body")]
    public async Task<IActionResult> GetFromBody([FromBody] Guid id)
    {
        MemberDomain member = await _repo.GetAsync(id);

        if (member == null)
        {
            return NotFound();
        }

        MemberDto memberDto = new()
        {
            Name = member.Name,
            Address = member.Address,
            FromAttribute = member.FromAttribute,
        };

        return Ok(memberDto);
    }

}
