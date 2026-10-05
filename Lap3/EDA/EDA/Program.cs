using System;
using EDA.Events;
using EDA.Services;
using EDA;

internal class Program
{
    private static void Main(string[] args)
    {
        // Khởi tạo EventBus
        var eventBus = new EventBus();

        // Khởi tạo các service
        var fraudDetectionService = new FraudDetectionService(eventBus);
        var analyticsService = new AnalyticsService(eventBus);

        // Nhập thông tin rút tiền
        Console.Write("Nhập mã tài khoản rút tiền: ");
        string withdrawAccountId = Console.ReadLine();

        Console.Write("Nhập số tiền rút: ");
        double withdrawAmount = double.Parse(Console.ReadLine());

        var withdrawEvent = new MoneyWithdrawEvent
        {
            AccountId = withdrawAccountId,
            Amount = withdrawAmount
        };
        eventBus.Publish(withdrawEvent);

        // Nhập thông tin chuyển tiền
        Console.Write("Nhập tài khoản chuyển đi: ");
        string fromAccount = Console.ReadLine();

        Console.Write("Nhập tài khoản nhận: ");
        string toAccount = Console.ReadLine();

        Console.Write("Nhập số tiền chuyển: ");
        double transferAmount = double.Parse(Console.ReadLine());

        var transferEvent = new TransferMoneyEvent
        {
            FromAccount = fromAccount,
            ToAccount = toAccount,
            Amount = transferAmount
        };
        eventBus.Publish(transferEvent);

        Console.WriteLine("Events published.");
    }
}
