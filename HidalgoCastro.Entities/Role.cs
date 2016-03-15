using FluentValidation;
using FluentValidation.Attributes;
using System.Collections.Generic;

namespace HidalgoCastro.Entities
{
    [Validator(typeof(RoleValidator))]
    public class Role : Timespan
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Permission> Permissions { get; set; }

    }

    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(x => x.Id);
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
