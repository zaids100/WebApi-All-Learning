using API.Application;
using API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Infrastructure
{
    public class UserService
    {
        private readonly IUserPost<User, int> _userRepository;
        public UserService(IUserPost<User, int> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<User> AddUser(User user)
        {
            return await _userRepository.Add(user);
        }

        public async Task<User> Update(User entity) {
            return await _userRepository.Update(entity);
        }
    }
}
