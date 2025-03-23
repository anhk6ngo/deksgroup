namespace DTour.Client.Shared.Validators;

public class RailCreateBookingValidator : AbstractValidator<RailCreateBooking>
{
    public RailCreateBookingValidator()
    {
        RuleFor(rq => rq.Buyer).SetValidator(new RailBuyerValidator()!);
        RuleForEach(rq => rq.Passengers).SetValidator(new RailPassengerValidator());
    }
}

public class RailPassengerValidator : AbstractValidator<RailPassenger>
{
    public RailPassengerValidator()
    {
        RuleFor(rq => rq.FirstName).NotEmpty().WithMessage("The First Name' Pax{CollectionIndex} field is required").MaximumLength(255);
        RuleFor(rq => rq.LastName).NotEmpty().WithMessage("The Last Name' Pax{CollectionIndex} field is required").MaximumLength(255);
    }
}

public class RailBuyerValidator : AbstractValidator<RailBuyer>
{
    public RailBuyerValidator()
    {
        RuleFor(rq => rq.FirstName).NotEmpty().WithMessage("The First Name field is required").MaximumLength(255);
        RuleFor(rq => rq.LastName).NotEmpty().WithMessage("The First Name field is required").MaximumLength(255);
        RuleFor(rq => rq.Email).NotEmpty().WithMessage("The Email field is required").MaximumLength(255);
        RuleFor(rq => rq.Phone).NotEmpty().WithMessage("The Phone field is required").MaximumLength(20);
    }
}