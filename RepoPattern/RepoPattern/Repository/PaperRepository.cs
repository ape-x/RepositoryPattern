using Microsoft.EntityFrameworkCore;
using RepoPattern.Entities;

namespace RepoPattern.Repository;

public class PaperRepository : GenericRepository<Paper>
{
    public PaperRepository(DbContext context) : base(context)
    {
    }
}

