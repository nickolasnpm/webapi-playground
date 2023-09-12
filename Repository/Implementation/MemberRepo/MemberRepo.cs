using Microsoft.EntityFrameworkCore;
using webapi_playground.Data;
using webapi_playground.Models.Domain.Member;
using webapi_playground.Repository.Interface.MemberRepo;

namespace webapi_playground.Repository.Implementation.MemberRepo;

public class MemberRepo : IMemberRepo
{
    private readonly SchoolMemberDbContext _dbContext;

    public MemberRepo(SchoolMemberDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<MemberDomain>> GetAllAsync()
    {
        return await _dbContext.MemberTable.ToListAsync();
    }

    public async Task<MemberDomain> GetAsync(Guid id)
    {
        return await _dbContext.MemberTable.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<MemberDomain> AddAsync(MemberDomain member)
    {
        member.Id = Guid.NewGuid();
        await _dbContext.MemberTable.AddAsync(member);
        await _dbContext.SaveChangesAsync();
        return member;
    }

    public async Task<MemberDomain> UpdateAsync(Guid id, MemberDomain member)
    {
        MemberDomain? existing = await _dbContext.MemberTable.FindAsync(id);

        if (existing != null)
        {
            existing.Name = member.Name;
            existing.Address = member.Address;
            existing.FromAttribute = member.FromAttribute;
            await _dbContext.SaveChangesAsync();
        }

        return existing;
    }

    public async Task<MemberDomain> DeleteAsync (Guid id)
    {
        MemberDomain? member= await _dbContext.MemberTable.FindAsync(id);

        if(member != null)
        {
            _dbContext.MemberTable.Remove(member);
            await _dbContext.SaveChangesAsync();
        }
        
        return member;
    }
}
