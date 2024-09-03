using Checkbox.Fdm.Core.Enums;
using Checkbox.Fdm.Core.PosModels.Actions;
using Checkbox.Fdm.Core.PosModels.Checkbox;
using Checkbox.Fdm.UseCases;

namespace FpsUsecases.PosExample;

public partial class PosExample
{
    /// <summary>
    /// Print of a copy of ticket (prebill) P 22/53
    /// </summary>
    /// <param name="cancellationToken"></param>
    private async Task RunUseCaseM161(CancellationToken cancellationToken = default)
    {
        //Create the correct action according to the example
        var newCopyAction = new PosCopyAction(
            FpsFinancesModels.Company,
            _myPos567,
            FpsFinancesModels.TerminalTer1Bar,
            FpsFinancesModels.EmployeeJohn)
        {
            SalesActionNumber = 1014,
            BookingDate = DateTime.Now,
            BookingPeriodId = Guid.Parse("dffcd829-a0e5-41ca-a0ae-9eb887f95637"),
            TicketMedium = TicketMedium.PAPER,
            Reference = new CheckboxSignReference
            {
                Checkbox = FpsFinancesModels.Checkbox01,
                DateTime = new DateTime(2024, 7, 29, 12, 57, 12),
                Eventlabel = EventLabel.P,
                EventCounter = 22,
                TotalCounter = 53
            }
        };

        //Sign the action
        var result = await checkboxService.SignPosAction(newCopyAction, false, null, cancellationToken);

        //Handle the result accordingly
        PrintResult(result.SignResult, result.Errors);
    }
}