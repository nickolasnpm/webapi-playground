using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi_playground.Models.Domain.Member;
using webapi_playground.Models.DTOs.Member;
using webapi_playground.Repository.Interface.MemberRepo;

namespace webapi_playground.Controllers.HttpRequest;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MemberPostController : ControllerBase
{
    private readonly IMemberRepo _repo;

    public MemberPostController(IMemberRepo repo)
    {
        _repo = repo;
    }

    [HttpPost("from-query")]
    public async Task<IActionResult> CreateFromQuery([FromQuery] MemberDto personDto)
    {

        MemberDomain member = new()
        {
            Name = personDto.Name,
            Address = personDto.Address,
            FromAttribute = "From Query",
        };

        member = await _repo.AddAsync(member);

        return Ok(member);
    }

    [HttpPost("from-header")]
    public async Task<IActionResult> CreateFromHeader([FromHeader] MemberDto memberDto)
    {

        MemberDomain member = new()
        {
            Name = memberDto.Name,
            Address = memberDto.Address,
            FromAttribute = "From Header",
        };

        member = await _repo.AddAsync(member);

        return Ok(member);
    }

    [HttpPost("from-form")]
    public async Task<IActionResult> CreateFromFrom([FromForm] MemberDto memberDto)
    {

        MemberDomain member = new()
        {
            Name = memberDto.Name,
            Address = memberDto.Address,
            FromAttribute = "From Form",
        };

        member = await _repo.AddAsync(member);

        return Ok(member);
    }

    [HttpPost("from-route")]
    public async Task<IActionResult> CreateFromRoute([FromRoute] MemberDto memberDto)
    {

        MemberDomain member = new()
        {
            Name = memberDto.Name,
            Address = memberDto.Address,
            FromAttribute = "From Route",
        };

        member = await _repo.AddAsync(member);

        return Ok(member);
    }

    [HttpPost("from-body")]
    public async Task<IActionResult> CreateFromBody([FromBody] MemberDto memberDto)
    {

        MemberDomain member = new()
        {
            Name = memberDto.Name,
            Address = memberDto.Address,
            FromAttribute = "From Body",
        };

        member = await _repo.AddAsync(member);

        return Ok(member);
    }

}
