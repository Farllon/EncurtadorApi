using System.Threading.Tasks;
using Encurtador.Data;
using Encurtador.Intefaces;
using Encurtador.Models;
using MongoDB.Driver;

namespace Encurtador.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly UrlshortnerContext _context;

        public UrlRepository(UrlshortnerContext context)
        {
            _context = context;
        }

        public async Task Create(Url url)
        {
            await _context.Urls.InsertOneAsync(url);
        }

        public async Task<bool> Delete(string hash)
        {
            FilterDefinition<Url> filter = Builders<Url>.Filter.Eq(m => m.Hash, hash);
            DeleteResult deleteResult = await _context
                                        .Urls
                                        .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<Url> GetUrl(string hash)
        {
            FilterDefinition<Url> filter = Builders<Url>.Filter.Eq(m => m.Hash, hash);
            return await _context
                .Urls
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Url url)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Urls
                        .ReplaceOneAsync(
                            filter: g => g.Hash == url.Hash,
                            replacement: url);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}