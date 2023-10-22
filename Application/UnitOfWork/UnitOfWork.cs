using Application.Repository;
using Domain.Interfaces;
using Persistence;
namespace Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DbAppContext context;
    private RolRepo _roles;
    private UserRepo _users;

    public UnitOfWork(DbAppContext _context)
    {
        context = _context;
    }

    public IRol Rols
    {
        get
        {
            if (_roles == null)
            {
                _roles = new RolRepo(context);
            }
            return _roles;
        }
    }

    public IUser Users
    {
        get
        {
            if (_users == null)
            {
                _users = new UserRepo(context);
            }
            return _users;
        }
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public async Task<int> SaveAsync()
    {
        return await context.SaveChangesAsync();
    }
}