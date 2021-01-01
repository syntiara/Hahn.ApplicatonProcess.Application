using System.Runtime.Serialization;

namespace Hahn.ApplicatonProcess.December2020.Data.Entities
{
    /// <summary>
    ///     A base class for any model that reasonably can be compared by id.
    /// </summary>

    public class BaseEntity
    {
        /// <summary>
        ///     An id for this model that is unique across all models of this type.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
    }
}
