using Candidates.Repositories.Dto;
using CandidatesDataAccess.Model;

namespace Candidates.Repositories.Interface
{
    public interface ICandidateRepository
    {
        Task<Candidate> GetCandidateByEmailAsync(string email);
        Task<CandidateOutputDto> CreateOrEditCandidateAsync(CandidateDto candidateDto);
    }
}
