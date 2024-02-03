using API_CRUD_Template.Data.Interfaces;
using API_CRUD_Template.Models;
using Microsoft.EntityFrameworkCore;

namespace API_CRUD_Template.Data.Services
{
    public class FirstEntityService : IFirstEntity
    {
        private readonly AppDbContext _context;

        public FirstEntityService(AppDbContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            var ent = await _context.FirstEntities.SingleOrDefaultAsync(e => e.Id == id);
            _context.FirstEntities.Remove(ent);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FirstEntity>> GetAll()
        {
            return await _context.FirstEntities.ToListAsync();
        }

        public async Task<FirstEntity> GetById(int id)
        {
            var ent = await _context.FirstEntities.Where(e => e.Id == id).SingleOrDefaultAsync();
            return ent;
        }

        public async Task<FirstEntity> Post(FirstEntity firstEntity)
        {
            var ent = new FirstEntity()
            {
                Id = firstEntity.Id,
                Name = firstEntity.Name
            };
            await _context.AddAsync(ent);
            await _context.SaveChangesAsync();
            return ent;
        }

        public async Task<FirstEntity> Update(int id, FirstEntity firstEntity)
        {
            var existingEnt = await _context.FirstEntities.FindAsync(id);
            if (existingEnt == null)
            {
                return null;
            }
            existingEnt.Name = firstEntity.Name;

            _context.Update(existingEnt);
            _context.SaveChanges();
            return existingEnt;
        }
    }
}
