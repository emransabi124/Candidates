using CandidatesDataAccess.Identity;
using CandidatesDataAccess.Model;
using Candidates.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Candidates.Repositories.Dto;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;

namespace Candidates.Repositories.Service
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CandidateRepository> _logger; // Use generic ILogger
        private readonly IMemoryCache _cache;
        private const string CandidateCacheKey = "Candidate_";
        public CandidateRepository(ApplicationDbContext context, IMapper mapper, ILogger<CandidateRepository> logger, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
        }

        public async Task<Candidate> GetCandidateByEmailAsync(string email)
        {
            try
            {
                if (_cache.TryGetValue(CandidateCacheKey + email, out Candidate candidate))
                {
                    return candidate;
                }

                candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);

                if (candidate != null)
                {
                    _cache.Set(CandidateCacheKey + email, candidate, TimeSpan.FromMinutes(15));
                }

                return candidate;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CandidateOutputDto> CreateOrEditCandidateAsync(CandidateDto candidateDto)
        {
            try
            {
                // Serialize candidateDto to JSON
                string candidateJson = JsonSerializer.Serialize(candidateDto);
                _logger.LogInformation("Start Create Or Edit Candidate :: {CandidateJson}", candidateJson);

                var existingCandidate = await GetCandidateByEmailAsync(candidateDto.Email);
                if (existingCandidate != null)
                {
                    _logger.LogInformation("Editing Candidate with Email: {Email}", candidateDto.Email);

                    _mapper.Map(candidateDto, existingCandidate);
                    _context.Candidates.Update(existingCandidate);
                    _cache.Set(CandidateCacheKey + candidateDto.Email, existingCandidate, TimeSpan.FromMinutes(15));
                    _logger.LogInformation("Set Cache in Update Email ::  ", CandidateCacheKey + candidateDto.Email);

                }
                else
                {
                    _logger.LogInformation("Creating New Candidate :: {CandidateJson}", candidateJson);

                    var newCandidate = _mapper.Map<Candidate>(candidateDto);
                    await _context.Candidates.AddAsync(newCandidate);
                    _cache.Set(CandidateCacheKey + candidateDto.Email, existingCandidate, TimeSpan.FromMinutes(15));
                    _logger.LogInformation("Set Cache in Create Email ::  ", CandidateCacheKey + candidateDto.Email);

                }

                var result = new CandidateOutputDto
                {
                    ResultSave = await _context.SaveChangesAsync()
                };

                _logger.LogInformation("Successfully Created or Edited Candidate :: {CandidateJson}", candidateJson);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while Creating or Editing Candidate :: {CandidateJson}", JsonSerializer.Serialize(candidateDto));
                throw;
            }
        }
    }
}
