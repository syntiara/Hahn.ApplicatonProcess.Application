using System;
using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Hahn.ApplicatonProcess.December2020.Domain.Models;

namespace Hahn.ApplicatonProcess.December2020.Domain
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicantWDTO, Applicant>().ReverseMap();
            CreateMap<ApplicantDTO, Applicant>().ReverseMap();

        }
    }
}


