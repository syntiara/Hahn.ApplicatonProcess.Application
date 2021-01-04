using System;
namespace Hahn.ApplicatonProcess.December2020.Domain.Models
{
    /// <summary>
    /// Data transfer object for readonly requests
    /// </summary>
    public class ApplicantDTO : ApplicantWDTO
    {
        /// <summary>
        ///     An id for this model.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
    }
}
