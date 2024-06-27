using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.Infra;
using GestaoDeResiduos.Responses;

namespace GestaoDeResiduos.Repositories.Impl
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DatabaseContext _context;

        public Repository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResponse<T>> GetAllAsync(int page = 1, int size = 10)
        {
            var totalItems = await _context.Set<T>().CountAsync();
            var items = await _context.Set<T>()
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PaginatedResponse<T>
            {
                Items = items,
                PageNumber = page,
                PageSize = size,
                TotalCount = totalItems
            };
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException($"Entidade de ID {id} não encontrada.");
            }
            return entity;
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}