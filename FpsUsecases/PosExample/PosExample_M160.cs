using Checkbox.Fdm.Core.Enums;
using Checkbox.Fdm.Core.PosModels.Actions;
using Checkbox.Fdm.Core.PosModels.Checkbox;
using Checkbox.Fdm.UseCases;

namespace FpsUsecases.PosExample;

public partial class PosExample
{
    /// <summary>
    /// Print of a copy of ticket (sale) N 16/52
    /// </summary>
    /// <param name="cancellationToken"></param>
    private async Task RunUseCaseM160(CancellationToken cancellationToken = default)
    {
        //Create the correct action according to the example
        var newCopyAction = new PosCopyAction(
            FpsFinancesModels.Company,
            _myPos567,
            FpsFinancesModels.TerminalTer1Bar,
            FpsFinancesModels.EmployeeJohn)
        {
            SalesActionNumber = 1013,
            BookingDate = DateTime.Now,
            BookingPeriodId = Guid.Parse("dffcd829-a0e5-41ca-a0ae-9eb887f95637"),
            TicketMedium = TicketMedium.PAPER,
            Reference = new CheckboxSignReference
            {
                Checkbox = FpsFinancesModels.Checkbox01,
                DateTime = new DateTime(2024, 7, 9, 11, 48, 04),
                Eventlabel = EventLabel.N,
                EventCounter = 16,
                TotalCounter = 52
            }
        };

        //Sign the action
        var result = await checkboxService.SignPosAction(newCopyAction, false, null, cancellationToken);

        //Handle the result accordingly
        PrintResult(result.SignResult, result.Errors);
    }
}