using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankMISTues
{
    public class BankAccount
    {
        public int Accountno { get; private set; }
        public string OwnerName { get; private set; }
        public decimal Balance { get; private set; }


        public BankAccount(int accountno, string ownerName, decimal initalBalance = 0 )
        
        {
            Accountno = accountno;
            OwnerName = ownerName;
            Balance = initalBalance;

        }

        //Essential Methods 
        //Deposit

        public void Deposit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Amount should be positive");
                return;
            }

            Balance += amount;
            Console.WriteLine($"Deposited {amount} , New Balance is {Balance}");

        }


        public void Withdraw(decimal amount)
        {
            if (amount <= 0 || amount > Balance)
            {
                Console.WriteLine("Amont should be possitive ");
            }


            Balance -= amount;
            Console.WriteLine($"Withdrawed {amount} , New Balance is {Balance}");

        }



        public void DisplayBalance()
        {
            Console.WriteLine($" Account No {Accountno}  for {OwnerName}  has {Balance}");
        }

    }
}
