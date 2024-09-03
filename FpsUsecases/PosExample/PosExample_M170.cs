using Checkbox.Fdm.Core.PosModels.Actions;
using Checkbox.Fdm.Core.PosModels;
using Checkbox.Fdm.UseCases;

namespace FpsUsecases.PosExample;

public partial class PosExample
{
    /// <summary>
    /// Registration of "Work In" of an employee (employeeId 75061189702) in training modus
    /// </summary>
    /// <param name="cancellationToken"></param>
    private async Task RunUseCaseM170(CancellationToken cancellationToken = default)
    {
        //Create the correct action according to the example
        var newWorkInAction = new PosWorkAction(
            FpsFinancesModels.Company,
            _myPos567,
            FpsFinancesModels.TerminalTer1Bar,
            FpsFinancesModels.EmployeeJohn)
        {
            SalesActionNumber = 1015,
            BookingDate = DateTime.Now,
            BookingPeriodId = Guid.Parse("dffcd829-a0e5-41ca-a0ae-9eb887f95637"),
            Type = WorkType.WORK_IN
        };

        //Sign the action
        var result = await checkboxService.SignPosAction(newWorkInAction, true, null, cancellationToken);

        //Handle the result accordingly
        PrintResult(result.SignResult, result.Errors);
    }
}