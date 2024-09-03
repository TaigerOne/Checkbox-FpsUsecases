using Checkbox.Fdm.Core.Enums;
using Checkbox.Fdm.Core.PosModels.Actions.Transaction;
using Checkbox.Fdm.Core.PosModels.Actions;
using Checkbox.Fdm.Core.PosModels.Checkbox;
using Checkbox.Fdm.Core.PosModels.Money;
using Checkbox.Fdm.UseCases;

namespace FpsUsecases.PosExample;

public partial class PosExample
{
    /// <summary>
    /// Dining room:
    ///    Refund ticket (full refund):  
    ///    Products on original ticket N 1000/1200:
    ///    - 2 Dry Martini (unit price 12 EUR, VAT code A) 
    ///    - 1 Burger of the Chef (unit price 28 EUR, VAT code B)
    ///
    /// </summary>
    /// <param name="cancellationToken"></param>
    private async Task RunUseCaseM111(CancellationToken cancellationToken = default)
    {
        //Create the correct action according to the example
        var newSalesAction = new PosSalesAction(
            FpsFinancesModels.Company,
            _myPos567,
            FpsFinancesModels.TerminalTer2Din,
            FpsFinancesModels.EmployeeElisa)
        {
            TicketMedium = TicketMedium.PAPER,
            SalesActionNumber = 1001,
            BookingDate = DateTime.Now,
            BookingPeriodId = Guid.Parse("dffcd829-a0e5-41ca-a0ae-9eb887f95637"),
            CheckboxReference = new CheckboxSignReference
            {
                Checkbox = FpsFinancesModels.Checkbox01,
                DateTime = DateTime.Now,
                Eventlabel = EventLabel.N,
                EventCounter = 1000,
                TotalCounter = 1200
            },
            TransactionLines =
            [
                new RefundLine(TransactionLineType.SINGLE_PRODUCT, 2, FpsFinancesModels.ProdDryMartini),
                new RefundLine(TransactionLineType.SINGLE_PRODUCT, 1, FpsFinancesModels.ProdBurgerOfTheChef)
            ],
            Payments = [
                new Payment
                {
                    Id = "1",
                    Name = "CONTANT",
                    Type = PaymentType.CASH,
                    InputMethod = InputMethod.MANUAL,
                    Amount = 52,
                    AmountType = PaymentLineType.PAYMENT
                }
            ]
        };

        //Sign the action
        var result = await checkboxService.SignPosAction(newSalesAction, false, null, cancellationToken);

        //Handle the result accordingly
        PrintResult(result.SignResult, result.Errors);
    }
}