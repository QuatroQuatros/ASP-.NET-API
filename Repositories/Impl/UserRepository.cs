using AutoMapper;
using GestaoDeResiduos.Infra;
using GestaoDeResiduos.Models;
using GestaoDeResiduos.ViewModels.Responses;
using Microsoft.EntityFrameworkCore;

namespace GestaoDeResiduos.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<PaginatedResponse<UserViewModelResponse>> GetUsersPaginatedAsync(int pageNumber, int pageSize)
        {
            var usersQuery = _context.Users.AsQueryable();

            var totalCount = await usersQuery.CountAsync();
            var users = await usersQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var userViewModels = _mapper.Map<List<UserViewModelResponse>>(users);

            return new PaginatedResponse<UserViewModelResponse>
            {
                Items = userViewModels,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
        
        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }

            return user;
        }


        public async Task<UserModel> AddUserAsync(UserModel user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        
        public async Task<UserModel> UpdateUserAsync(UserModel user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(UserModel user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }


    }
}
