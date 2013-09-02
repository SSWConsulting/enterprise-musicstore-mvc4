using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Northwind.MusicStore.Domain
{
    public interface IAnimal
    {
        void eat();
        decimal weight { get; set; }
    }

    public interface IBird
    {
        void fly();
        
    }

    public interface IFeatheredBird : IBird
    {
        int NumberOfFeaters { get; set; }
        int Age { get; set; }
    }

    public interface fish
    {
        void swim();
    }

    public class Crow : IAnimal, IFeatheredBird
    {
        public void eat()
        {
            
        }

        public decimal weight { get; set; }

        public void fly()
        {
            
        }

        public int NumberOfFeaters { get; set; }
        public int Age { get; set; }
    }

    public class AnimalProcessor
    {
        public AnimalProcessor(IBird someBird)
        {
            someBird.fly();
        }
    }

    public interface INotificationProvider
    {
        void Send(string to, string message);
    }

    public class SmsSender : INotificationProvider
    {
        public void Send(string to, string message)
        {
            
        }
    }
    public class EmailSender: INotificationProvider
    {
        public void Send(string to, string message)
        {

        }
    }

    
    internal class websiteorbusinesslayerclass
    {
        public websiteorbusinesslayerclass()
        {
            
        }
        void process()
        {
            var processor = new OrderProcessor(new EmailSender());
        }
    }

    class OrderProcessor
    {
        INotificationProvider notifier;

        public OrderProcessor(INotificationProvider notificationProvider)
        {
            notifier = notificationProvider;
        }

        public void ProcessOrders()
        {
            //summ up all the items in the order, save things to database

            //send an email to customer
            notifier.Send("adam@stephensen.me","Your order is ready");
        }
    }
}
