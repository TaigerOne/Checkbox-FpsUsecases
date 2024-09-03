using Checkbox.Fdm.Core.Enums;
using Checkbox.Fdm.Core.PosModels.Actions;
using Checkbox.Fdm.Core.PosModels.Checkbox;
using Checkbox.Fdm.Core.PosModels.Money;
using Checkbox.Fdm.UseCases;

namespace FpsUsecases.PosExample;

public partial class PosExample
{
    /// <summary>
    /// PaymentCorrection
    /// Payment of N14/50 (total to be paid 52 EUR) was made by CARD_CREDIT (Mastercard via EFT) and not in cash (as originally registered) 
    /// </summary>
    /// <param name="cancellationToken"></param>
    private async Task RunUseCaseM132(CancellationToken cancellationToken = default)
    {
        //Create the correct action according to the example
        var newPaymentCorrectionAction = new PosPaymentCorrectionAction(
            FpsFinancesModels.Company,
            _myPos567,
            FpsFinancesModels.TerminalTer1Bar,
            FpsFinancesModels.EmployeeJohn)
        {
            SalesActionNumber = 1008,
            BookingDate = DateTime.Now,
            BookingPeriodId = Guid.Parse("dffcd829-a0e5-41ca-a0ae-9eb887f95637"),
            CheckboxReference = new CheckboxSignReference
            {
                Checkbox = FpsFinancesModels.Checkbox01,
                DateTime = new DateTime(2024, 7, 29, 11, 35, 09),
                Eventlabel = EventLabel.N,
                EventCounter = 14,
                TotalCounter = 50
            },
            Payments =
            [
                new Payment
                {
                    Id = "1",
                    Name = "CONTANT",
                    Type = PaymentType.CASH,
                    InputMethod = InputMethod.MANUAL,
                    Amount = -52,
                    AmountType = PaymentLineType.PAYMENT,
                    CashDrawer = FpsFinancesModels.CashDrawer1
                },
                new Payment()
                {
                    Id = "2",
                    Name = "MASTERCARD",
                    Type = PaymentType.CARD_CREDIT,
                    InputMethod = InputMethod.AUTOMATIC,
                    Amount = 52,
                    AmountType = PaymentLineType.PAYMENT
                }
            ]
        };

        //Sign the action
        var result = await checkboxService.SignPosAction(newPaymentCorrectionAction, false, null, cancellationToken);

        //Handle the result accordingly
        PrintResult(result.SignResult, result.Errors);
    }
}