using FluentValidation;
using FluentValidation.Attributes;

namespace HidalgoCastro.Entities
{
    [Validator(typeof(PaginationValidator))]
    public class Pagination
    {

        public int Page { get; set; }

        public int Count { get; set; }

        public string Sort { get; set; }

        public string Search { get; set; }

        public string Order { get; set; }

    }

    public class PaginationValidator : AbstractValidator<Pagination>
    {
        public PaginationValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.Count).GreaterThan(0);
            RuleFor(x => x.Sort).NotNull().NotEmpty();
            RuleFor(x => x.Order).NotNull().NotEmpty();
            RuleFor(x => x.Search).NotNull();
        }
    }

}
