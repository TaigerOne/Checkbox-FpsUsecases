using Checkbox.Fdm.Core.PosModels.Actions;
using Checkbox.Fdm.Core.PosModels;
using Checkbox.Fdm.UseCases;

namespace FpsUsecases.PosExample;

public partial class PosExample
{
    /// <summary>
    /// Registration of "Work In" of employee (employeeId 80113078968)
    /// </summary>
    /// <param name="cancellationToken"></param>
    private async Task RunUseCaseM140(CancellationToken cancellationToken = default)
    {
        //Create the correct action according to the example
        var newWorkInAction = new PosWorkAction(
            FpsFinancesModels.Company,
            _myPos567,
            FpsFinancesModels.TerminalPos2Rec,
            FpsFinancesModels.EmployeeSammy)
        {
            SalesActionNumber = 1010,
            BookingDate = DateTime.Now,
            BookingPeriodId = Guid.Parse("dffcd829-a0e5-41ca-a0ae-9eb887f95637"),
            Type = WorkType.WORK_IN
        };

        //Sign the action
        var result = await checkboxService.SignPosAction(newWorkInAction, false, null, cancellationToken);

        //Handle the result accordingly
        PrintResult(result.SignResult, result.Errors);
    }
}