using System.Threading.Tasks;
using Encurtador.Data;
using Encurtador.Intefaces;
using Encurtador.Models;
using MongoDB.Driver;

namespace Encurtador.Repositories
{
    public class HashRepository : IHashRepository
    {
        private readonly UrlshortnerContext _context;

        public HashRepository(UrlshortnerContext context)
        {
            _context = context;
        }

        public async Task Create(Hash hash)
        {
            await _context.Hashes.InsertOneAsync(hash);
        }

        public async Task<bool> Delete(string hashCode)
        {
            FilterDefinition<Hash> filter = Builders<Hash>.Filter.Eq(m => m.HashCode, hashCode);
            DeleteResult deleteResult = await _context
                                        .Hashes
                                        .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public Task<Hash> GetHash(string hashCode)
        {
            FilterDefinition<Hash> filter = Builders<Hash>.Filter.Eq(m => m.HashCode, hashCode);

            return _context
                .Hashes
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Hash hash)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Hashes
                        .ReplaceOneAsync(
                            filter: g => g.HashCode == hash.HashCode,
                            replacement: hash);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}