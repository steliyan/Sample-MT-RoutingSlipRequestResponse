﻿using System;
using System.Threading.Tasks;
using Contracts;
using MassTransit;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq();

            try
            {
                await bus.StartAsync();

                var client = CreateRequestClient(bus);

                while (true)
                {
                    Console.Write("Enter order id (quit exits): ");

                    var orderId = Console.ReadLine();
                    if (orderId == "quit")
                    {
                        break;
                    }

                    try
                    {
                        var (completed, faulted) = await client.GetResponse<OrderCompleted, OrderFaulted>(new { Id = orderId });

                        if (completed.Status == TaskStatus.RanToCompletion)
                        {
                            var result = await completed;
                            Console.WriteLine("Order id: {0}; data: {1}", result.Message.Id, result.Message.Data);
                        }
                        else
                        {
                            var result = await faulted;
                            Console.WriteLine("Order id: {0}; faulted with a reason: {1}", result.Message.Id, result.Message.Reason);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error procesing the request: {0}", e.Message);
                    }
                }
            }
            finally
            {
                await bus.StopAsync();
            }
        }

        private static IRequestClient<OrderPlaced> CreateRequestClient(IBusControl busControl)
        {
            var serviceAddress = new Uri("exchange:OrderPlaced");
            var client = busControl.CreateRequestClient<OrderPlaced>(serviceAddress, TimeSpan.FromSeconds(10));

            return client;
        }
    }
}
