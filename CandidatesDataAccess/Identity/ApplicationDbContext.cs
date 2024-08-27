using CandidatesDataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CandidatesDataAccess.Identity
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Candidate> Candidates { get; set; }
    }
}
