using Microsoft.AspNetCore.Mvc;
using webapi_playground.Models.Domain.Member;
using webapi_playground.Repository.Interface.MemberRepo;

namespace webapi_playground.Controllers.HttpRequest;

[ApiController]
[Route("api/[controller]")]
public class MemberGetListController : ControllerBase
{

    private readonly IMemberRepo _repo;

    public MemberGetListController(IMemberRepo repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetPersonList()
    {
        IEnumerable<MemberDomain> memberList = await _repo.GetAllAsync();
        return Ok(memberList);
    }
}
