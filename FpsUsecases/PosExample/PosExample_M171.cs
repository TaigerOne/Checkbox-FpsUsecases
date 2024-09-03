using Checkbox.Fdm.Core.Enums;
using Checkbox.Fdm.Core.PosModels.Actions.Transaction;
using Checkbox.Fdm.Core.PosModels.Actions;
using Checkbox.Fdm.Core.PosModels.Money;
using Checkbox.Fdm.UseCases;

namespace FpsUsecases.PosExample;

public partial class PosExample
{
    /// <summary>
    /// Sale of
    ///     2 Perrier (unit price 2.50 EUR, VAT code A)
    ///     1 Burger of the Chef (unit price 28.00 EUR, VAT code B)
    /// in training mode. Paymentmethod: CASH
    /// </summary>
    /// <param name="cancellationToken"></param>
    private async Task RunUseCaseM171(CancellationToken cancellationToken = default)
    {
        //Create the correct action according to the example
        var newSalesAction = new PosSalesAction(
            FpsFinancesModels.Company,
            _myPos567,
            FpsFinancesModels.TerminalTer1Bar,
            FpsFinancesModels.EmployeeJohn)
        {
            TicketMedium = TicketMedium.NONE,
            SalesActionNumber = 1016,
            BookingDate = DateTime.Now,
            BookingPeriodId = Guid.Parse("dffcd829-a0e5-41ca-a0ae-9eb887f95637"),
            TransactionLines =
            [
                new SaleLine(TransactionLineType.SINGLE_PRODUCT, 2, FpsFinancesModels.ProdPerrier),
                new SaleLine(TransactionLineType.SINGLE_PRODUCT, 1, FpsFinancesModels.ProdBurgerOfTheChef)
            ],
            Payments = [
                new Payment
                {
                    Id = "1",
                    Name = "CONTANT",
                    Type = PaymentType.CASH,
                    InputMethod = InputMethod.MANUAL,
                    Amount = 33,
                    AmountType = PaymentLineType.PAYMENT
                }
            ]
        };

        //Sign the action
        var result = await checkboxService.SignPosAction(newSalesAction, true, null, cancellationToken);

        //Handle the result accordingly
        PrintResult(result.SignResult, result.Errors);
    }
}