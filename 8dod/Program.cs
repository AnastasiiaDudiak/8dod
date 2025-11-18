using System;

namespace Lab8_Dod2
{
    // Інтерфейс для всіх об'єктів, які можуть рухатися
    interface IMovable
    {
        void Move(int distance);   // рух на задану відстань
    }

    // Інтерфейс для всіх об'єктів, які можна заряджати
    interface IRechargeable
    {
        void Recharge(int percent);   // зарядити на певний відсоток
    }

    // Клас ElectricScooter реалізує ДВА інтерфейси: IMovable і IRechargeable
    class ElectricScooter : IMovable, IRechargeable
    {
        public string Name { get; set; }
        public int BatteryLevel { get; private set; }      // заряд (0–100%)
        public int TotalDistance { get; private set; }     // загальна пройдена відстань

        public ElectricScooter(string name, int startBattery)
        {
            Name = name;
            BatteryLevel = startBattery;
            TotalDistance = 0;
        }

        // Реалізація методу Move з інтерфейсу IMovable
        public void Move(int distance)
        {
            if (BatteryLevel <= 0)
            {
                Console.WriteLine($"{Name}: немає заряду, не можу рухатися!");
                return;
            }

            TotalDistance += distance;
            BatteryLevel -= distance;   // спрощено: 1 км = 1% батареї

            if (BatteryLevel < 0) BatteryLevel = 0;

            Console.WriteLine($"{Name} проїхав {distance} км. Всього: {TotalDistance} км. Заряд: {BatteryLevel}%");
        }

        // Реалізація методу Recharge з інтерфейсу IRechargeable
        public void Recharge(int percent)
        {
            BatteryLevel += percent;
            if (BatteryLevel > 100) BatteryLevel = 100;

            Console.WriteLine($"{Name} зарядився на {percent}%. Тепер заряд: {BatteryLevel}%");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Створюємо один електросамокат
            ElectricScooter scooter = new ElectricScooter("Xiaomi Scooter", 30);

            Console.WriteLine("Демонстрацiя роботи iнтерфейсів IMovable та IRechargeable\n");

            // Виклики через змінну типу класу
            scooter.Move(5);
            scooter.Move(10);

            scooter.Recharge(20);
            scooter.Move(15);

            Console.WriteLine();

            // Показуємо, що той самий об'єкт можна бачити як IMovable та як IRechargeable
            IMovable m = scooter;          // посилання типу IMovable
            IRechargeable r = scooter;     // посилання типу IRechargeable

            m.Move(3);                     // виклик через інтерфейс руху
            r.Recharge(50);                // виклик через інтерфейс зарядки

            Console.WriteLine("\nНатиснiть будь-яку клавiшу для завершення...");
            Console.ReadKey();
        }
    }
}
