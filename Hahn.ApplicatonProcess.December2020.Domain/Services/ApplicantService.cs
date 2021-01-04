using System;
using System.Collections.Generic;
using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Hahn.ApplicatonProcess.December2020.Data.Repository;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Hahn.ApplicatonProcess.December2020.Domain.Service
{
    /// <summary>
    ///    Implementation of <see cref="IApplicantService"/>
    /// </summary>
    public class ApplicantService: IApplicantService
    {
        private readonly IRepository<Applicant> applicantRepo;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicantService"/> class.
        /// </summary>
        /// <param name="applicantRepo">The repository to use.</param>
        /// <param name="logger">The logger to use.</param>
        /// <param name="mapper">the mapping profile</param>
        public ApplicantService(IRepository<Applicant> applicantRepo, ILogger<ApplicantService> logger, IMapper mapper)
        {
            this.applicantRepo = applicantRepo;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <inheritdoc />
        public IEnumerable<ApplicantDTO> GetApplicants()
        {
            var res = applicantRepo.GetAll();
            return mapper.Map<IList<ApplicantDTO>>(res);
        }

        /// <inheritdoc />
        public ApplicantDTO GetApplicantById(int id)
        {
           var res = applicantRepo.GetById(id);
           return mapper.Map<ApplicantDTO>(res);
        }

        /// <inheritdoc />
        public int InsertApplicant(ApplicantWDTO model)
        {
            try
            {
                var entity = mapper.Map<Applicant>(model);
                applicantRepo.Insert(entity);
                return entity.Id;
            }
            catch (Exception ex)
            {
                logger.LogError("Unsuccessful applicant creation", ex);
                throw;
            }
        }

        /// <inheritdoc />
        public ApplicantWDTO UpdateApplicant(int id, ApplicantWDTO model)
        {
            try
            {
                var entity = applicantRepo.GetById(id);

                if (entity != null)
                {
                    mapper.Map<Applicant, ApplicantWDTO>(entity, model);
                    applicantRepo.Update(entity);
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError("Unsuccessful applicant update", ex);
                throw;
            }
        }

        /// <inheritdoc />
        public bool DeleteApplicant(int id)
        {
            try
            {
                var entity = applicantRepo.GetById(id);
                if (entity != null)
                {
                    applicantRepo.Delete(entity);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                logger.LogError("Unsuccessful applicant update", ex);
                throw;

            }
        }
    }
}
