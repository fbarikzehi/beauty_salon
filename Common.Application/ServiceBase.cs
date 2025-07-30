using Microsoft.EntityFrameworkCore;

namespace Common.Application
{
    public class ServiceBase<TDbContext> 
        where TDbContext : DbContext, new()
    {
        public TDbContext DbContext { get; set; } = new TDbContext();
    }
}
