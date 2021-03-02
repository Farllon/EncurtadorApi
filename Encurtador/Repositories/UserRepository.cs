using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Encurtador.Data;
using Encurtador.Intefaces;
using Encurtador.Models;
using MongoDB.Driver;

namespace Encurtador.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UrlshortnerContext _context;

        public UserRepository(UrlshortnerContext context)
        {
            _context = context;
        }

        public async Task Create(User user)
        {
            await _context.Users.InsertOneAsync(user);
        }

        public async Task<bool> Delete(Guid id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                                        .Users
                                        .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<User> GetUser(Guid id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(m => m.Id, id);

            return await _context
                .Users
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetByCred(string user, string password)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(m => m.Username, user) & 
                Builders<User>.Filter.Eq(m => m.Password, password);

            return await _context
                .Users
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Update(User user)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Users
                        .ReplaceOneAsync(
                            filter: g => g.Id == user.Id,
                            replacement: user);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}