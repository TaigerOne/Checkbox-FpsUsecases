using Checkbox.Fdm.Core.Models.Messages;
using Checkbox.Fdm.Core.PosModels;
using Checkbox.Fdm.Sdk;
using Checkbox.Fdm.UseCases;
using Microsoft.Extensions.Logging;

namespace FpsUsecases.PosExample;

public partial class PosExample(ICheckboxService checkboxService, ILogger<PosExample> logger)
{
    private readonly PosSystem _myPos567 = FpsFinancesModels.Pos567;

    public async Task InitPos(CancellationToken cancellationToken = default)
    {
        //This is an optional step
        //In the example there are 2 checkboxes connected, you can change this to your own checkbox Id's
        //TODO Add your Checkbox Id's here
        _myPos567.CheckboxUnits[0].CheckboxId = "YOUR CHECKBOX 1 ID";
        _myPos567.CheckboxUnits[1].CheckboxId = "YOUR CHECKBOX 2 ID";

        logger.LogInformation("Initializing the POS System");

        // Try to Initializes the Checkbox devices,
        // This configures their destination network addresses automatically
        await checkboxService.InitializeCheckboxesAsync(
            _myPos567.CheckboxIdentifiers,
            CheckboxLoadbalancingMode.Manual,
            cancellationToken);
    }

    public async Task RunPos(CancellationToken cancellationToken = default)
    {
        while (true)
        {
            Console.WriteLine("A: M110: Direct sale of 2 products");
            Console.WriteLine("B: M111: Refund ticket (full refund)");
            Console.WriteLine("C: M112: Partial Refund ticket");
            Console.WriteLine("D: M121: Order on table 1");
            Console.WriteLine("E: M122: Transfer order from table 1 to table 5");
            Console.WriteLine("F: M123: A Pre Bill for the customer");
            Console.WriteLine("G: M130: 100€ transfer to Cash Drawer 1");
            Console.WriteLine("H: M131: Drawer 1 is opened without action");
            Console.WriteLine("I: M132: Payment Correction for Payment of N14/50");
            Console.WriteLine("J: M133: Declaration 1000€ in Drawer 1");
            Console.WriteLine("K: M140: Register Work In (80113078968)");
            Console.WriteLine("L: M141: Register Work Out (84022899837)");
            Console.WriteLine("M: M150: Invoice for customer BE0308357159)");
            Console.WriteLine("N: M160: Print a copy of ticket N16/52");
            Console.WriteLine("O: M161: Print a copy of ticket P22/53");
            Console.WriteLine("P: M170: Training: Register Work In (75061189702)");
            Console.WriteLine("R: M171: Training: Direct sale of 2 products");
            Console.WriteLine("S: M172: Training: print of a copy of ticket N35/73");
            Console.WriteLine("Your choice (Q = Quit)?");

            var readKey = Console.ReadKey(intercept: true);

            if (readKey.Key == ConsoleKey.Q)
            {
                Console.WriteLine("Exiting...");
                break;
            }

            switch (readKey.Key)
            {
                case ConsoleKey.A:
                    await RunUseCaseM110(cancellationToken);
                    break;
                case ConsoleKey.B:
                    await RunUseCaseM111(cancellationToken);
                    break;
                case ConsoleKey.C:
                    await RunUseCaseM112(cancellationToken);
                    break;
                case ConsoleKey.D:
                    await RunUseCaseM121(cancellationToken);
                    break;
                case ConsoleKey.E:
                    await RunUseCaseM122(cancellationToken);
                    break;
                case ConsoleKey.F:
                    await RunUseCaseM123(cancellationToken);
                    break;
                case ConsoleKey.G:
                    await RunUseCaseM130(cancellationToken);
                    break;
                case ConsoleKey.H:
                    await RunUseCaseM131(cancellationToken);
                    break;
                case ConsoleKey.I:
                    await RunUseCaseM132(cancellationToken);
                    break;
                case ConsoleKey.J:
                    await RunUseCaseM133(cancellationToken);
                    break;
                case ConsoleKey.K:
                    await RunUseCaseM140(cancellationToken);
                    break;
                case ConsoleKey.L:
                    await RunUseCaseM141(cancellationToken);
                    break;
                case ConsoleKey.M:
                    await RunUseCaseM150(cancellationToken);
                    break;
                case ConsoleKey.N:
                    await RunUseCaseM160(cancellationToken);
                    break;
                case ConsoleKey.O:
                    await RunUseCaseM161(cancellationToken);
                    break;
                case ConsoleKey.P:
                    await RunUseCaseM170(cancellationToken);
                    break;
                case ConsoleKey.R:
                    await RunUseCaseM171(cancellationToken);
                    break;
                case ConsoleKey.S:
                    await RunUseCaseM172(cancellationToken);
                    break;
                default:
                    Console.WriteLine("Invalid Choice, try again...");
                    break;
            }

            Console.WriteLine("Your choice (Q = Quit)?");
        }
    }

    private void PrintResult(SignResult? signResult, List<SignError>? signErrors)
    {
        if (signResult != null)
        {
            Console.WriteLine($"-----------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"--  {signResult.FdmRef.EventLabel} {signResult.FdmRef.EventCounter}/{signResult.FdmRef.TotalCounter}   {signResult.EventOperation} SIGNED BY: {signResult.FdmRef.FdmId}:{signResult.FdmSwVersion} ON {signResult.FdmRef.FdmDateTime}");
            Console.WriteLine($"-----------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"--  Pos               : {signResult.PosId}");
            Console.WriteLine($"--  Terminal          : {signResult.TerminalId}");
            Console.WriteLine($"--  Pos Ticket Number : {signResult.PosFiscalTicketNo}");
            Console.WriteLine($"-----------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"--  Signature         : {signResult.DigitalSignature}");
            Console.WriteLine($"--  Short Signature   : {signResult.ShortSignature}");
            Console.WriteLine($"--  Short Url         : {signResult.VerificationUrl}");
            Console.WriteLine($"-----------------------------------------------------------------------------------------------------------------");
        }

        if (signErrors != null && signErrors.Count > 0)
        {
            Console.WriteLine($"-- {signErrors.Count} ERRORS");
            foreach (var signError in signErrors)
            {
                Console.WriteLine($"-- {signError.Category} - {signError.Code}");
            }
            Console.WriteLine($"-----------------------------------------------------------------------------------------------------------------");
        }
    }
}