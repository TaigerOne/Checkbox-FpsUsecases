using Checkbox.Fdm.Core.Enums;
using Checkbox.Fdm.Core.PosModels.Actions.Transaction;
using Checkbox.Fdm.Core.PosModels.Actions;
using Checkbox.Fdm.Core.PosModels.Costcenters;
using Checkbox.Fdm.UseCases;

namespace FpsUsecases.PosExample;

public partial class PosExample
{
    /// <summary>
    /// Prebill for the order:
    ///     2 Dry Martini (unit price 12 EUR, VAT code A)
    ///     4 Tapas variation (unit price 3,34 EUR, VAT code B)
    /// on table 1 (BAR)  is printed
    /// </summary>
    /// <param name="cancellationToken"></param>
    private async Task RunUseCaseM123(CancellationToken cancellationToken = default)
    {
        //Create the correct action according to the example
        var newPreBillAction = new PosPreBillAction(
            FpsFinancesModels.Company,
            _myPos567,
            FpsFinancesModels.TerminalTer1Bar,
            FpsFinancesModels.EmployeeJohn)
        {
            SalesActionNumber = 1005,
            BookingDate = DateTime.Now,
            BookingPeriodId = Guid.Parse("dffcd829-a0e5-41ca-a0ae-9eb887f95637"),
            TransactionLines =
            [
                new SaleLine(TransactionLineType.SINGLE_PRODUCT, 2, FpsFinancesModels.ProdDryMartini),
                new SaleLine(TransactionLineType.SINGLE_PRODUCT, 4, FpsFinancesModels.ProdTapasVariation)
            ],
            Costcenter = new CostcenterAssignment
            {
                Costcenter = FpsFinancesModels.CostcenterT1
            }
        };

        //Sign the action
        var result = await checkboxService.SignPosAction(newPreBillAction, false, null, cancellationToken);

        //Handle the result accordingly
        PrintResult(result.SignResult, result.Errors);
    }
}