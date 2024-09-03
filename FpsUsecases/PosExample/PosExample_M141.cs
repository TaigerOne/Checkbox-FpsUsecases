using Checkbox.Fdm.Core.PosModels.Actions;
using Checkbox.Fdm.Core.PosModels;
using Checkbox.Fdm.UseCases;

namespace FpsUsecases.PosExample;

public partial class PosExample
{
    /// <summary>
    /// Registration of "Work Out" of employee (employeeId 84022899837)
    /// </summary>
    /// <param name="cancellationToken"></param>
    private async Task RunUseCaseM141(CancellationToken cancellationToken = default)
    {
        //Create the correct action according to the example
        var newWorkOutAction = new PosWorkAction(
            FpsFinancesModels.Company,
            _myPos567,
            FpsFinancesModels.TerminalTer1Bar,
            FpsFinancesModels.EmployeeElisa)
        {
            SalesActionNumber = 1011,
            BookingDate = DateTime.Now,
            BookingPeriodId = Guid.Parse("dffcd829-a0e5-41ca-a0ae-9eb887f95637"),
            Type = WorkType.WORK_OUT
        };

        //Sign the action
        var result = await checkboxService.SignPosAction(newWorkOutAction, false, null, cancellationToken);

        //Handle the result accordingly
        PrintResult(result.SignResult, result.Errors);
    }
}