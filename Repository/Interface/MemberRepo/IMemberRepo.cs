using webapi_playground.Models.Domain.Member;

namespace webapi_playground.Repository.Interface.MemberRepo;

public interface IMemberRepo
{
    Task<IEnumerable<MemberDomain>> GetAllAsync();
    Task<MemberDomain> GetAsync(Guid id);
    Task<MemberDomain> AddAsync(MemberDomain member);
    Task<MemberDomain> UpdateAsync(Guid id, MemberDomain member);
    Task<MemberDomain> DeleteAsync (Guid id);
}
