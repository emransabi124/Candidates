using CandidatesDataAccess.Model;

namespace Candidates.Repositories.Interface
{
    public interface ICandidateRepository
    {
        Task<Candidate> GetCandidateByEmailAsync(string email);
        Task CreateOrEditCandidateAsync(Candidate candidate);
    }
}
