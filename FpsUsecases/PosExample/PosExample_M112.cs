
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
    /// BAR:
    /// Partial refund:
    /// * refund of 1 Dry Martini (unit price 12 EUR, VAT code A)
    /// * sale of 1 Burger of the Chef (unit price 15 EUR, VAT code B)
    /// Payment by cash.    /// </summary>
    /// <param name="cancellationToken"></param>
    private async Task RunUseCaseM112(CancellationToken cancellationToken = default)
    {
        //Create the correct action according to the example
        var newSalesAction = new PosSalesAction(
            FpsFinancesModels.Company,
            _myPos567,
            FpsFinancesModels.TerminalTer1Bar,
            FpsFinancesModels.EmployeeElisa)
        {
            TicketMedium = TicketMedium.PAPER,
            SalesActionNumber = 1002,
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
                new RefundLine(TransactionLineType.SINGLE_PRODUCT, 1, FpsFinancesModels.ProdDryMartini),
                new SaleLine(TransactionLineType.SINGLE_PRODUCT, 1, FpsFinancesModels.ProdBurgerOfTheChef)
            ],
            Payments =
            [
                new Payment
                {
                    Id = "1",
                    Name = "CONTANT",
                    Type = PaymentType.CASH,
                    InputMethod = InputMethod.MANUAL,
                    Amount = 16,
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