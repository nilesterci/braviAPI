using Bravi.Infrastructure.Session;
using Microsoft.Extensions.Configuration;

namespace Bravi.Infrastructure.Repository
{
    public abstract class BaseRepository
    {
        protected DbSession _session;

        public BaseRepository(DbSession session)
        {
            _session = session;
        }

        protected long GetOffset(int pageIndex, int pageSize)
        {
            return (pageIndex - 1) * pageSize;
        }
    }
}