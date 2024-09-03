using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace BankMISTues
{
    class Program
    {


        private  static List<BankAccount> accounts = new List<BankAccount>();
        private static int nextAccNumber = 1001;

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1": CreatAccount();
                        break;
                    case "2": PerfomeDeposit();
                        break;
                    case "3":
                        PerfomeWithdraw();
                        break;
                    case "4": CheckBalance();
                        break;
                    case "5": DisplayAccount();
                        break;
                    case "6": 
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Thanks for using our system");
                        break;
                }
            }

        }

        private static void DisplayAccount()
        {
            throw new NotImplementedException();
        }

        private static void CheckBalance()
        {
            throw new NotImplementedException();
        }

        private static void PerfomeWithdraw()
        {
            throw new NotImplementedException();
        }

        private static void PerfomeDeposit()
        {
          

        }


        public static void FindAccoung()
        {


            Console.WriteLine("Enter Your Accound Number");
            if (int.TryParse(Console.ReadLine()?.Trim(), out int acc ))
            {

                return accounts.Find(accc => acc.Accountno == acc);
            }

            Console.WriteLine("Invalid Accound Number");
            return null;
        }




        private static void CreatAccount()
        {
            Console.WriteLine("Enter Owner Name:");
            String name = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Empty and Null name NA");  
                return;
            }
            decimal initBalance = 0;
            Console.WriteLine("Do have some initial cash");
            string initCash = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(initCash) )
            {

                Console.WriteLine("no init cash then bal is 0");
                initCash = "0";
            }


     
            
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("Hey Boss , Welcome: ");
            Console.WriteLine("1. creating account ");
            Console.WriteLine("2.Deposit: ");
            Console.WriteLine("3.Withdraw ");
            Console.WriteLine("4.Check Balance");
            Console.WriteLine("5.All Accounts");
            Console.WriteLine("6. Leave");
          

        }
    }

}

// Enhanced on 2025-10-19 - Commit 2
