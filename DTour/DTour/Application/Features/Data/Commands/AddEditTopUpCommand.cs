using DTour.Client.Agent.Pages.Catalog;
using Microsoft.AspNetCore.Identity;

namespace DTour.Application.Features.Data.Commands;

public class AddEditTopUpCommand : IRequest<ResultClient<Guid>>
{
    public AddEditDataRequest<TopUpDto> Request { get; set; } = default!;
}

internal class AddEditTopUpCommandHandler(
    IUnitOfWork<Guid, PortalContext> unitOfWork)
    : IRequestHandler<AddEditTopUpCommand, ResultClient<Guid>>
{
    public async Task<ResultClient<Guid>> Handle(AddEditTopUpCommand command,
        CancellationToken cancellationToken)
    {
        switch (command.Request.Action)
        {
            case ActionCommandType.Add:
            case ActionCommandType.AddApprove:
                var oNewItem = command.Request.Data.Adapt<TopUp>();
                await unitOfWork.RepositoryNew<TopUp>().AddAsync(oNewItem);
                //Add Amount to User Balance
                if (command.Request.Action == ActionCommandType.AddApprove && oNewItem.Status == 2)
                {
                    await AddEditTopUp(oNewItem);
                }

                await unitOfWork.Commit(cancellationToken);
                return await ResultClient<Guid>.SuccessAsync(oNewItem.Id, "The item added");
            default:
                var currentItem = await unitOfWork.RepositoryNew<TopUp>()
                    .GetByIdAsync(command.Request.Data!.Id);
                if (currentItem != null)
                {
                    if (command.Request.Action != ActionCommandType.Approve && currentItem.Status == 2)
                    {
                        return await ResultClient<Guid>.FailAsync("Not found the item");
                    }

                    command.Request.Data.Adapt(currentItem);
                    await unitOfWork.RepositoryNew<TopUp>().UpdateAsync(currentItem);
                    //Add Amount to User Balance
                    if (command.Request.Action == ActionCommandType.Approve && currentItem.Status == 2)
                    {
                        await AddEditTopUp(currentItem);
                    }

                    await unitOfWork.Commit(cancellationToken);
                    return await ResultClient<Guid>.SuccessAsync(currentItem.Id, "The item updated");
                }

                break;
        }

        return await ResultClient<Guid>.FailAsync("Not found the item");
    }

    private async Task AddEditTopUp(TopUp data)
    {
        if ($"{data.UserId}".IsNullOrEmpty()) return;
        var userId = data.UserId.ToGuid();
        var oFind = await unitOfWork.RepositoryAgg<UserBalance>().GetByIdAsync(userId);
        if (oFind == null)
        {
            oFind = new UserBalance()
            {
                Id = userId,
                Amount = (data.IsDeposit ? 0 : data.ApproveAmount),
                DepositAmount = (data.IsDeposit ? data.ApproveAmount : 0)
            };
            await unitOfWork.RepositoryAgg<UserBalance>().AddAsync(oFind);
        }
        else
        {
            if (data.IsDeposit)
            {
                oFind.DepositAmount += data.ApproveAmount;
            }
            else
            {
                oFind.Amount += data.ApproveAmount;
            }

            await unitOfWork.RepositoryAgg<UserBalance>().UpdateAsync(oFind);
        }
    }
}