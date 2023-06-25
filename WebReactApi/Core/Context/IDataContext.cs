using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebReactApi.Core.Context
{
    public interface IDataContext : IDisposable
    {
        int SaveChanges();
        IEnumerable<dynamic> DynamicListFromSql(string Sql, params object[] parameters);
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
