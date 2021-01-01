using System;
using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Hahn.ApplicatonProcess.December2020.Domain.Models;

namespace Hahn.ApplicatonProcess.December2020.Domain.Models
{
    /// <summary>
    /// Mapping profile to map models to domain objects and vice-versa
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperProfile"/> class.
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<ApplicantWDTO, Applicant>().ReverseMap();
            CreateMap<ApplicantDTO, Applicant>().ReverseMap();

        }
    }
}


