using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
namespace Application.Repository;

public class RolRepo : Generic<Rol>, IRol
{
    protected readonly DbAppContext _context;

    public RolRepo(DbAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Rol>> GetAllAsync()
    {
        return await _context.Rols
            //.Include(p => p.)
            .ToListAsync();
    }

    public override async Task<Rol> GetByIdAsync(int id)
    {
        return await _context.Rols
            //.Include(p => p.)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}