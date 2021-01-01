using System;
using System.Collections.Generic;
using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Hahn.ApplicatonProcess.December2020.Domain.Models;

namespace Hahn.ApplicatonProcess.December2020.Domain.Service
{
    /// <summary>
    ///     Defines the logic for <see cref="ApplicantService"/> class.
    /// </summary>
    public interface IApplicantService
    {
        /// <summary>
        /// Gets all applicants
        /// </summary>
        /// <returns><see cref="ApplicantDTO"/>s</returns>
        IEnumerable<ApplicantDTO> GetApplicants();

        /// <summary>
        /// Gets an applicant based on id
        /// </summary>
        /// <returns><see cref="ApplicantDTO"/></returns>
        ApplicantDTO GetApplicantById(int id);

        /// <summary>
        /// Creates a new applicant
        /// </summary>
        /// <param name="model"><see cref="ApplicantWDTO"/>The applicant to create</param>
        /// <returns>id of the new applicant</returns>
        int InsertApplicant(ApplicantWDTO model);

        /// <summary>
        /// Updates an existing applicant
        /// </summary>
        /// <param name="id">id of the applicant to update</param>
        /// <param name="model">The new applicant model</param>
        /// <returns><see cref="ApplicantWDTO"/></returns>
        ApplicantWDTO UpdateApplicant(int id, ApplicantWDTO model);

        /// <summary>
        /// Deletes an existing applicant
        /// </summary>
        /// <param name="id">The id of the applicant to delete</param>
        /// <returns>boolean value of true/false</returns>
        bool DeleteApplicant(int id);
    }
}
