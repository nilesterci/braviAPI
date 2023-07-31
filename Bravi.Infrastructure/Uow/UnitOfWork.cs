using Bravi.Domain.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bravi.Infrastructure.Session;

namespace Bravi.Infrastructure.Uow
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DbSession _session;

        public DbSession Session => _session;

        public UnitOfWork(DbSession session)
        {
            _session = session;
        }

        public void BeginTransaction()
        {
            Session.Transaction = Session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            Session.Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            Session.Transaction.Rollback();
            Dispose();
        }

        public void Dispose() => Session.Transaction?.Dispose();
    }
}
