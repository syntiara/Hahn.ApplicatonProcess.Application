using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Hahn.ApplicatonProcess.December2020.Domain.ServiceClients;

namespace Hahn.ApplicatonProcess.December2020.Domain.Models
{
    /// <summary>
    ///     Validation rule applied to the <see cref="ApplicantWDTO"/> class.
    /// </summary>

    public class ApplicantValidator : AbstractValidator<ApplicantWDTO>
   {
       private readonly ICountryClient client;
       /// <summary>
       /// Initializes a new instance of the <see cref="ApplicantValidator"/> class.
       /// </summary>
       public ApplicantValidator(ICountryClient client)
       {
           this.client = client;

           RuleFor(x => x.Hired).NotNull();
           RuleFor(x => x.Name).NotNull().Length(5, 50);
           RuleFor(x => x.FamilyName).NotNull().Length(5, 50);
           RuleFor(x => x.Address).Length(10, 50);
           RuleFor(x => x.EmailAddress).NotNull().Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
           RuleFor(x => x.Age).NotNull().InclusiveBetween(20, 60);
           RuleFor(x => x.CountryOforigin).Must(isCountryValid).WithMessage("Invalid country provided");
       }

        private bool isCountryValid(string name)
        {
            if(!string.IsNullOrEmpty(name)){
                var country = client.GetCountry(name);
                return !string.IsNullOrEmpty(country.Result) ? true : false;
            }
            return false;
        }
   }
}