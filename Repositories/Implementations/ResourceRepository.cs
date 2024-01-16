using Microsoft.EntityFrameworkCore;
using SimpleBookingSystem.Data;
using SimpleBookingSystem.Models;
using SimpleBookingSystem.Repositories.Interfaces;

namespace SimpleBookingSystem.Repositories.Implementations;

public class ResourceRepository: IResourceRepository
{
    private readonly DataContext _context;

    public ResourceRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Resource>> GetAllAsync()
    {
        return await _context.Resources.ToListAsync();
    }
    
    public async Task<Resource> GetByIdAsync(int id)
    {
        var resource = await _context.Resources.FindAsync(id);
        return resource;
    }

    public Task AddAsync(Resource entity)
    {
        throw new NotImplementedException();
    }
}