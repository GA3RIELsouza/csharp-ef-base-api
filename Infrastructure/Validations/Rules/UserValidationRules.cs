using FluentValidation;

namespace Base_API.Infrastructure.Validations.Rules
{
    public static class UserValidationRules
    {
        public static IRuleBuilderOptions<T, string> NameRules<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("The username is required.")
                .MinimumLength(2).WithMessage("The username cannot be shorter than 2 characters.")
                .MaximumLength(64).WithMessage("The username cannot be longer than 64 characters.");
        }

        public static IRuleBuilderOptions<T, string> EmailRules<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("The e-mail is required.")
                .MinimumLength(3).WithMessage("The e-mail cannot be shorter than 3 characters.")
                .MaximumLength(254).WithMessage("The e-mail cannot be longer than 254 characters.")
                .EmailAddress().WithMessage("The e-mail format is invalid.");
        }

        public static IRuleBuilderOptions<T, string> PasswordRules<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("The password is required.")
                .MinimumLength(8).WithMessage("The password cannot be shorter than 8 characters.");
        }
    }
}
