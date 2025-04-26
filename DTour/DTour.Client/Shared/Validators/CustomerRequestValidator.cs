namespace DTour.Client.Shared.Validators;

public class CustomerRequestValidator : AbstractValidator<CustomerRequestDto>
{
    public CustomerRequestValidator()
    {
        RuleFor(rq => rq.FullName).NotEmpty().WithMessage("The Full Name field is required").MaximumLength(255);
        RuleFor(rq => rq.Title).NotEmpty().WithMessage("The Subject field is required").MaximumLength(255);
        RuleFor(rq => rq.Email).NotEmpty().WithMessage("The Email field is required").MaximumLength(255).EmailAddress();
        RuleFor(rq => rq.Message).NotEmpty().WithMessage("The Message field is required").MaximumLength(2000);
    }
}