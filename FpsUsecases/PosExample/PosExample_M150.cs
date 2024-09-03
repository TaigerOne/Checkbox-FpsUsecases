using Checkbox.Fdm.Core.Enums;
using Checkbox.Fdm.Core.PosModels.Actions;
using Checkbox.Fdm.Core.PosModels.Checkbox;
using Checkbox.Fdm.Core.PosModels.Costcenters;
using Checkbox.Fdm.UseCases;

namespace FpsUsecases.PosExample;

public partial class PosExample
{
    /// <summary>
    /// Invoice for customer (Id 100 - customerVatNo BE0308357159)
    /// for tickets 
    ///     N 14/50
    ///     N 16/52 
    /// </summary>
    /// <param name="cancellationToken"></param>
    private async Task RunUseCaseM150(CancellationToken cancellationToken = default)
    {
        //Create the correct action according to the example
        var newInvoiceAction = new PosInvoiceAction(
            FpsFinancesModels.Company,
            _myPos567,
            FpsFinancesModels.TerminalTer1Bar,
            FpsFinancesModels.EmployeeJohn)
        {
            SalesActionNumber = 1012,
            BookingDate = DateTime.Now,
            BookingPeriodId = Guid.Parse("dffcd829-a0e5-41ca-a0ae-9eb887f95637"),
            TicketMedium = TicketMedium.PAPER,
            CustomerVatNumber = "BE0308357159",
            InvoiceNumber = "20240005",
            Costcenter = new CostcenterAssignment
            {
                Costcenter = new Costcenter
                {
                    Id = "100",
                    Type = CostCenterType.CUSTOMER,
                    Reference = "FIN"
                }
            },
            References = [
                new CheckboxSignReference
                {
                    Checkbox = FpsFinancesModels.Checkbox01,
                    DateTime = new DateTime(2024, 4, 29, 11, 35, 09),
                    Eventlabel = EventLabel.N,
                    EventCounter = 14,
                    TotalCounter = 50
                },
                new CheckboxSignReference
                {
                    Checkbox = FpsFinancesModels.Checkbox01,
                    DateTime = new DateTime(2024, 7, 29, 11, 48, 04),
                    Eventlabel = EventLabel.N,
                    EventCounter = 16,
                    TotalCounter = 52
                }
            ]
        };

        //Sign the action
        var result = await checkboxService.SignPosAction(newInvoiceAction, false, null, cancellationToken);

        //Handle the result accordingly
        PrintResult(result.SignResult, result.Errors);
    }
}