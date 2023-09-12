using Microsoft.AspNetCore.Mvc;
using webapi_playground.Models.Domain.Member;
using webapi_playground.Repository.Interface.MemberRepo;

namespace webapi_playground.Controllers.HttpRequest;

[ApiController]
[Route("api/[controller]")]
public class MemberDeleteController : ControllerBase
{
    private readonly IMemberRepo _repo;

    public MemberDeleteController(IMemberRepo repo)
    {
        _repo = repo;
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        MemberDomain member = await _repo.DeleteAsync(id);

        if (member == null)
        {
            return NotFound();
        }

        return Ok(member);
    }
}
