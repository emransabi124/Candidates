using AutoMapper;
using Candidates.Repositories.Dto;
using CandidatesDataAccess.Model;

namespace Candidates.Repositories.Mapper
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CandidateDto, Candidate>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id since it's auto-generated
        }
    }
}
