using CandidatesDataAccess.Identity;
using CandidatesDataAccess.Model;
using Candidates.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Candidates.Repositories.Dto;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Candidates.Repositories.Service
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CandidateRepository> _logger; // Use generic ILogger

        public CandidateRepository(ApplicationDbContext context, IMapper mapper, ILogger<CandidateRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Candidate> GetCandidateByEmailAsync(string email)
        {
            return await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
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
                }
                else
                {
                    _logger.LogInformation("Creating New Candidate :: {CandidateJson}", candidateJson);

                    var newCandidate = _mapper.Map<Candidate>(candidateDto);
                    await _context.Candidates.AddAsync(newCandidate);
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
