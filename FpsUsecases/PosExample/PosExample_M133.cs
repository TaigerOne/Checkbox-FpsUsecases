using Checkbox.Fdm.Core.Enums;
using Checkbox.Fdm.Core.PosModels.Actions;
using Checkbox.Fdm.Core.PosModels.Money;
using Checkbox.Fdm.UseCases;

namespace FpsUsecases.PosExample;

public partial class PosExample
{
    /// <summary>
    /// Declaration of 1000 EUR cash in drawer 1
    /// </summary>
    /// <param name="cancellationToken"></param>
    private async Task RunUseCaseM133(CancellationToken cancellationToken = default)
    {
        //Create the correct action according to the example
        var newMoneyTransferAction = new PosMoneyTransferAction(
            FpsFinancesModels.Company,
            _myPos567,
            FpsFinancesModels.TerminalTer1Bar,
            FpsFinancesModels.EmployeeJohn)
        {
            SalesActionNumber = 1009,
            BookingDate = DateTime.Now,
            BookingPeriodId = Guid.Parse("dffcd829-a0e5-41ca-a0ae-9eb887f95637"),
            MoneyTransfers =
            [
                new MoneyTransfer
                {
                    Id = "1",
                    Name = "CONTANT",
                    Type = PaymentType.CASH,
                    Amount = 1000,
                    InputMethod = InputMethod.MANUAL,
                    AmountType = MoneyInOutLineType.DRAWER_DECLARATION,
                    CashDrawer = FpsFinancesModels.CashDrawer1
                }
            ]
        };

        //Sign the action
        var result = await checkboxService.SignPosAction(newMoneyTransferAction, false, null, cancellationToken);

        //Handle the result accordingly
        PrintResult(result.SignResult, result.Errors);
    }
}