using Tazkara.Data;
using Tazkara.IRepository;

namespace Tazkara.Repository
{
    public class ApplicationUserRepository : IApplicationUser
    {
        private readonly ApplicationDbContext context;
        public ApplicationUserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
    }
}
