using Checkbox.Fdm.Core.Enums;
using Checkbox.Fdm.Core.PosModels.Actions;
using Checkbox.Fdm.Core.PosModels.Checkbox;
using Checkbox.Fdm.UseCases;

namespace FpsUsecases.PosExample;

public partial class PosExample
{
    /// <summary>
    /// In training mode: print of a copy of ticket (sale) N 35/73
    /// </summary>
    /// <param name="cancellationToken"></param>
    private async Task RunUseCaseM172(CancellationToken cancellationToken = default)
    {
        //Create the correct action according to the example
        var newCopyAction = new PosCopyAction(
            FpsFinancesModels.Company,
            _myPos567,
            FpsFinancesModels.TerminalTer1Bar,
            FpsFinancesModels.EmployeeJohn)
        {
            SalesActionNumber = 1017,
            BookingDate = DateTime.Now,
            BookingPeriodId = Guid.Parse("dffcd829-a0e5-41ca-a0ae-9eb887f95637"),
            TicketMedium = TicketMedium.NONE,
            Reference = new CheckboxSignReference
            {
                Checkbox = FpsFinancesModels.Checkbox02,
                DateTime = new DateTime(2024, 7, 3, 16, 40, 39),
                Eventlabel = EventLabel.N,
                EventCounter = 35,
                TotalCounter = 73
            }
        };

        //Sign the action
        var result = await checkboxService.SignPosAction(newCopyAction, true, null, cancellationToken);

        //Handle the result accordingly
        PrintResult(result.SignResult, result.Errors);
    }
}