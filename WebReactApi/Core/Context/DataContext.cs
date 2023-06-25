using Microsoft.EntityFrameworkCore;
using WebReactApi.Core.Entities;

namespace WebReactApi.Core.Context
{
    public class DataContext : DbContext, IDataContext
    {
      
        public DbSet<Todo> Todo { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
           
        }
        
        public IEnumerable<dynamic> DynamicListFromSql(string Sql, params object[] parameters)
        {
            return new List<dynamic>();
        }

      
        public async Task<int> SaveChangesAsync()
        {
            return await SaveChangesAsync(CancellationToken.None);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

       
    }
}
