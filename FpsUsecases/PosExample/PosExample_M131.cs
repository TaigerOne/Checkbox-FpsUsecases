using Checkbox.Fdm.Core.PosModels.Actions;
using Checkbox.Fdm.UseCases;

namespace FpsUsecases.PosExample;

public partial class PosExample
{
    /// <summary>
    /// Drawer 1 is opened without any action
    /// </summary>
    /// <param name="cancellationToken"></param>
    private async Task RunUseCaseM131(CancellationToken cancellationToken = default)
    {
        //Create the correct action according to the example
        var newDrawerOpenAction = new PosDrawerOpenAction(
            FpsFinancesModels.Company,
            _myPos567,
            FpsFinancesModels.TerminalTer1Bar,
            FpsFinancesModels.EmployeeJohn)
        {
            SalesActionNumber = 1007,
            BookingDate = DateTime.Now,
            BookingPeriodId = Guid.Parse("dffcd829-a0e5-41ca-a0ae-9eb887f95637"),
            CashDrawer = FpsFinancesModels.CashDrawer1
        };

        //Sign the action
        var result = await checkboxService.SignPosAction(newDrawerOpenAction, false, null, cancellationToken);

        //Handle the result accordingly
        PrintResult(result.SignResult, result.Errors);
    }
}