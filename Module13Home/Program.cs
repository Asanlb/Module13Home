using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static void Main()
    {
        BankService bankService = new BankService();
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("1. Добавить клиента в очередь");
            Console.WriteLine("2. Обслужить следующего клиента");
            Console.WriteLine("3. Выйти из программы");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите ИИН клиента: ");
                    string id = Console.ReadLine();

                    Console.WriteLine("Выберите тип обслуживания:");
                    Console.WriteLine("1. Кредитование");
                    Console.WriteLine("2. Открытие вклада");
                    Console.WriteLine("3. Консультация");
                    Console.Write("Введите номер услуги: ");

                    if (int.TryParse(Console.ReadLine(), out int serviceType))
                    {
                        ServiceType type = (ServiceType)serviceType;
                        bankService.EnqueueClient(new Client(id, type));
                        Console.WriteLine($"Клиент {id} добавлен в очередь для {type}.");
                        bankService.DisplayQueue();
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод.");
                    }
                    break;

                case "2":
                    Client nextClient = bankService.ServeNextClient();
                    if (nextClient != null)
                    {
                        Console.WriteLine($"Клиент {nextClient.Id} обслужен.");
                        bankService.DisplayQueue();
                    }
                    else
                    {
                        Console.WriteLine("Очередь пуста.");
                    }
                    break;

                case "3":
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
            }

            Console.WriteLine();
        }
    }
}

public enum ServiceType
{
    Credit,
    Deposit,
    Consultation
}

public class Client
{
    public string Id { get; }
    public ServiceType ServiceType { get; }

    public Client(string id, ServiceType serviceType)
    {
        Id = id;
        ServiceType = serviceType;
    }
}

public class BankService
{
    private Queue<Client> clientQueue = new Queue<Client>();

    public void EnqueueClient(Client client)
    {
        clientQueue.Enqueue(client);
    }

    public Client ServeNextClient()
    {
        return clientQueue.Count > 0 ? clientQueue.Dequeue() : null;
    }

    public void DisplayQueue()
    {
        Console.WriteLine("Текущая очередь:");

        foreach (var client in clientQueue)
        {
            Console.WriteLine($"{client.Id} - {client.ServiceType}");
        }

        Console.WriteLine();
    }
}

