using FluentValidation;
using FluentValidation.Attributes;

namespace HidalgoCastro.Entities
{
    [Validator(typeof(PaginationValidator))]
    public class Permission : Timespan
    {
        public int Id { get; set; }

        public string CodeName { get; set; }
    }

    public class PermissionValidator : AbstractValidator<Permission>
    {
        public PermissionValidator()
        {
            RuleFor(x => x.Id);
            RuleFor(x => x.CodeName).NotNull().NotEmpty();
        }
    }
}
