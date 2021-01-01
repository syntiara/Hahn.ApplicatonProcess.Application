namespace Hahn.ApplicatonProcess.December2020.Data.Entities
{
    /// <summary>
    ///     Basic information about an applicant.
    /// </summary>

    public class Applicant : BaseEntity
    {

        /// <summary>
        ///     Name of the applicant.
        /// </summary>
        /// <example>Chiamaka</example>
        public string Name { get; set; }

        /// <summary>
        ///     Family name of the applicant.
        /// </summary>
        /// <example>Okoliezeh</example>
        public string FamilyName { get; set; }

        /// <summary>
        ///     Address of the applicant.
        /// </summary>
        /// <example>Anambra, Nigeria</example>
        public string Address { get; set; }

        /// <summary>
        ///     Country of origin of the applicant.
        /// </summary>
        /// <example>Nigeria</example>
        public string CountryOforigin { get; set; }

        /// <summary>
        ///     Email address of the applicant.
        /// </summary>
        /// <example>me@example.com</example>
        public string EmailAddress { get; set; }

        /// <summary>
        ///     Age of the applicant.
        /// </summary>
        /// <example>28</example>
        public int Age { get; set; }

        /// <summary>
        ///     Determines if applicant is hired or not.
        /// </summary>
        /// <example>True</example>
        public bool Hired { get; set; } = false;

    }
}
