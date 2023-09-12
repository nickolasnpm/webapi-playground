using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi_playground.Models.Domain.Member;
using webapi_playground.Models.DTOs.Member;
using webapi_playground.Repository.Interface.MemberRepo;

namespace webapi_playground.Controllers.HttpRequest;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MemberPutController : ControllerBase
{
    private readonly IMemberRepo _repo;

    public MemberPutController(IMemberRepo repo)
    {
        _repo = repo;
    }

    [HttpPut("from-query")]
    public async Task<IActionResult> UpdateFromQuery([FromQuery] Guid id, MemberDto memberDto)
    {
        MemberDomain member = new()
        {
            Name = memberDto.Name,
            Address = memberDto.Address,
            FromAttribute = "From Query"
        };

        member = await _repo.UpdateAsync(id, member);

        if (member == null)
        {
            return NotFound();
        }

        return Ok(member);
    }

    [HttpPut("from-header")]
    public async Task<IActionResult> UpdateFromHeader([FromHeader] Guid id, MemberDto memberDto)
    {
        MemberDomain member = new()
        {
            Name = memberDto.Name,
            Address = memberDto.Address,
            FromAttribute = "From Header"
        };

        member = await _repo.UpdateAsync(id, member);

        if (member == null)
        {
            return NotFound();
        }

        return Ok(member);
    }

    [HttpPut("from-form")]
    public async Task<IActionResult> UpdateFromForm([FromForm] Guid id, MemberDto memberDto)
    {
        MemberDomain member = new()
        {
            Name = memberDto.Name,
            Address = memberDto.Address,
            FromAttribute = "From Form"
        };

        member = await _repo.UpdateAsync(id, member);

        if (member == null)
        {
            return NotFound();
        }

        return Ok(member);
    }

    [HttpPut("from-route")]
    public async Task<IActionResult> UpdateFromRoute([FromRoute] Guid id, MemberDto memberDto)
    {
        MemberDomain member = new()
        {
            Name = memberDto.Name,
            Address = memberDto.Address,
            FromAttribute = "From Route"
        };

        member = await _repo.UpdateAsync(id, member);

        if (member == null)
        {
            return NotFound();
        }

        return Ok(member);
    }

    [HttpPut("from-body")]
    public async Task<IActionResult> UpdateFromBody(Guid id, MemberDto memberDto)
    {
        MemberDomain member = new()
        {
            Name = memberDto.Name,
            Address = memberDto.Address,
            FromAttribute = "From Body"
        };

        member = await _repo.UpdateAsync(id, member);

        if (member == null)
        {
            return NotFound();
        }

        return Ok(member);
    }

}
