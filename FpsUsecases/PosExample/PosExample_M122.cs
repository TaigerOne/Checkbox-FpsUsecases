using Checkbox.Fdm.Core.Enums;
using Checkbox.Fdm.Core.PosModels.Actions.Transaction;
using Checkbox.Fdm.Core.PosModels.Actions;
using Checkbox.Fdm.Core.PosModels.Costcenters;
using Checkbox.Fdm.UseCases;

namespace FpsUsecases.PosExample;

public partial class PosExample
{
    /// <summary>
    /// BAR: Client orders:
    ///     2 Dry Martini (unit price 12 EUR, VAT code A)
    ///     4 Tapas variation (unit price 3,34 EUR, VAT code B)
    /// on table 1.
    /// </summary>
    /// <param name="cancellationToken"></param>
    private async Task RunUseCaseM122(CancellationToken cancellationToken = default)
    {
        //Create the correct action according to the example
        var newOrderTransferAction = new PosOrderTransferAction(
            FpsFinancesModels.Company,
            _myPos567,
            FpsFinancesModels.TerminalTer1Bar,
            FpsFinancesModels.EmployeeJohn)
        {
            SalesActionNumber = 1004,
            BookingDate = DateTime.Now,
            BookingPeriodId = Guid.Parse("dffcd829-a0e5-41ca-a0ae-9eb887f95637"),
            From =
            [
                new TransactionOnCostcenter
                {
                    Costcenter = new CostcenterAssignment { Costcenter = FpsFinancesModels.CostcenterT1 },
                    TransactionLines =
                    [
                        new TransferLine(TransactionLineType.SINGLE_PRODUCT, 1, FpsFinancesModels.ProdBurgerOfTheChef),
                        new TransferLine(TransactionLineType.SINGLE_PRODUCT, 1, FpsFinancesModels.ProdDryMartini)
                    ]
                }
            ],
            To =
            [
                new TransactionOnCostcenter
                {
                    Costcenter = new CostcenterAssignment { Costcenter = FpsFinancesModels.CostcenterT5 },
                    TransactionLines =
                    [
                        new SaleLine(TransactionLineType.SINGLE_PRODUCT, 1, FpsFinancesModels.ProdBurgerOfTheChef),
                        new SaleLine(TransactionLineType.SINGLE_PRODUCT, 1, FpsFinancesModels.ProdDryMartini)
                    ]
                }
            ]
        };

        //Sign the action
        var result = await checkboxService.SignPosAction(newOrderTransferAction, false, null, cancellationToken);

        //Handle the result accordingly
        PrintResult(result.SignResult, result.Errors);
    }
}