using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Crowler.Core.Actors;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;

namespace Crowler
{
   
    internal class Program
    {

        private static void Main(string[] args)
        {
            if (args[0].Equals("-s"))
                Akka_Send();
            if (args[0].Equals("-c"))
                Akka_Recieve();
        }

        private static void Akka_Send()
        {
            var system = ActorSystem.Create("MySystem");

            var greeter = system.ActorOf<ProductRequestActor>("greeter");

            Console.ReadLine();
        }

        private static void Akka_Recieve()
        {
            var system = ActorSystem.Create("MySystem");
            
            var greeter = system.ActorOf<ProductRequestActor>("greeter");
            
            Console.ReadLine();
        }


        private static void Kafka_Consume()
        {
            var options = new KafkaOptions(new Uri("http://127.0.0.1:9092"));
            var consumer = new Consumer(new ConsumerOptions("test", new BrokerRouter(options)));

            //Consume returns a blocking IEnumerable (ie: never ending stream)
            foreach (Message message in consumer.Consume())
            {
                Console.WriteLine("Response: P{0},O{1} : {2}",
                    message.Meta.PartitionId, message.Meta.Offset, System.Text.Encoding.UTF8.GetString(message.Value));
            }
        }

       

        private static void Kafka_Send()
        {
            var threads = new List<int>();
            for (int i = 0; i < 40; i++)
            {
                threads.Add(i);
            }


            Parallel.ForEach(threads,
                () => new Producer(new BrokerRouter(new KafkaOptions(new Uri("http://127.0.0.1:9092")))),
                (thread, loopstate, client) =>
                {
                    var respList = new List<Task<List<ProduceResponse>>>();

                    for (int i = 0; i < 200; i++)
                    {
                        respList.Add(
                            client.SendMessageAsync("test",
                                new[] {new Message(String.Format("Test message {0} {1}", thread, DateTime.UtcNow))}));
                    }
                    Task.WaitAll(respList.ToArray());

                    Console.WriteLine("Falted {0}", respList.Count(resp => resp.IsCanceled || resp.IsFaulted));

                    return client;
                },
                client => client.Dispose()
                );
        }
        
        
        private static void ActiveMQ_Send()
        {
            var threads = new List<int>();
            for (int i = 0; i < 40; i++)
            {
                threads.Add(i);
            }



            Parallel.ForEach(threads,
                () => new Producer(new BrokerRouter(new KafkaOptions(new Uri("http://127.0.0.1:9092")))),
                (thread, loopstate, client) =>
                {
                    var respList = new List<Task<List<ProduceResponse>>>();

                    for (int i = 0; i < 200; i++)
                    {
                        respList.Add(
                            client.SendMessageAsync("test",
                                new[] {new Message(String.Format("Test message {0} {1}", thread, DateTime.UtcNow))}));
                    }
                    Task.WaitAll(respList.ToArray());

                    Console.WriteLine("Falted {0}", respList.Count(resp => resp.IsCanceled || resp.IsFaulted));

                    return client;
                },
                client => client.Dispose()
                );
        }
    }
}