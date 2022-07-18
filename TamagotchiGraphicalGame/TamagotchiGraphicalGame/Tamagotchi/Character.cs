using System;
using System.Collections.Generic;

namespace TamagotchiConsoleGame.Tamagotchi
{
    [Serializable]
    public class Character
    {
        public string Name { get; set; }
        public string Stage { get; set; }
        public string TypeName { get; set; }
        public int Happiness { get; set; }
        public int Health { get; set; }
        public int Money { get; set; }
        public bool Ill { get; set; }
        public bool Alive { get; set; }
        public bool Tired { get; set; }
        public bool Asleep { get; set; }
        public int PoopCount { get; set; }
        public DateTime Created { get; set; }
        private RandomType randomType;

        public Character(string Name)
        {
            randomType = new RandomType();
            this.Name = Name;
            Stage = "Egg";
            TypeName = randomType.RandomEgg();
            Happiness = 4;
            Health = 4;
            Money = 2500;
            Ill = false;
            Alive = true;
            Tired = false;
            Asleep = false;
            PoopCount = 0;
            Created = DateTime.Now;
        }

        public Character(Character character)
        {
            this.randomType = character.randomType;
            this.Name = character.Name;
            this.Stage = character.Stage;
            this.TypeName = character.TypeName;
            this.Happiness = character.Happiness;
            this.Health = character.Health;
            this.Money = character.Money;
            this.Ill = character.Ill;
            this.Alive = character.Alive;
            this.Tired = character.Tired;
            this.Asleep = character.Asleep;
            this.PoopCount = character.PoopCount;
            this.Created = Created;
        }

        public void HealthDown()
        {
            Health -= 1;
            if (Health < 0)
            {
                Health = 0;
                FallSick();
            }
        }

        public void HealthUp(int amount)
        {
            Health += amount;
            if (Health > 4) Health = 4;
        }

        public void HappinessDown()
        {
            Happiness -= 1;
            if (Happiness < 0)
            {
                HealthDown();
                Happiness = 0;
            }
        }

        public void HappinessUp(int amount)
        {
            Happiness += amount;
            if (Happiness > 4) Happiness = 4;
        }

        public void MoneyUp(int amount)
        {
            Money += amount;
        }

        public bool MoneyDown(int amount)
        {
            if (Money >= amount)
            {
                Money -= amount;
                return true;
            }
            else return false;
        }

        public void FallSick()
        {
            Ill = true;
            Happiness = 0;
        }

        public void Recover()
        {
            Ill = false;
            HappinessUp(1);
            HealthUp(1);
        }

        public void Die()
        {
            Alive = false;
        }

        public void GetTired()
        {
            Tired = true;
        }

        public void FallAsleep()
        {
            Asleep = true;
        }

        public void WakeUp(string dayTime)
        {
            Asleep = false;
            if (dayTime == "day") Tired = false;
        }

        public void Poop()
        {
            ++PoopCount;
        }

        public void Clean()
        {
            PoopCount = 0;
        }

        public void Evolve()
        {
            switch (Stage)
            {
                case "Egg":
                    if (TypeName.StartsWith("B")) TypeName = randomType.NotRandomBaby("Boy");
                    else TypeName = randomType.NotRandomBaby("Girl");
                    Stage = "Baby";
                    break;
                case "Baby":
                    TypeName = randomType.RandomToodler();
                    Stage = "Toodler";
                    break;
                case "Toodler":
                    TypeName = randomType.RandomTeen();
                    Stage = "Teen";
                    break;
                case "Teen":
                    TypeName = randomType.RandomAdult();
                    Stage = "Adult";
                    break;
                case "Adult":
                    Die();
                    break;
            }
        }

        [Serializable]
        private class RandomType
        {
            private Dictionary<int, string> Eggs;
            private Dictionary<int, string> Babies;
            private Dictionary<int, string> Toodlers;
            private Dictionary<int, string> Teens;
            private Dictionary<int, string> Adults;
            private Random random;

            public RandomType()
            {
                AddEggs();
                AddBabies();
                AddToodlers();
                AddTeens();
                AddAdults();
                random = new Random();
            }

            public string RandomEgg()
            {
                return Eggs[random.Next(8)];
            }

            public string NotRandomBaby(string gender)
            {
                if (gender == "boy")
                {
                    return Babies[0];
                }
                else return Babies[1];
            }

            public string RandomToodler()
            {
                return Toodlers[random.Next(4)];
            }

            public string RandomTeen()
            {
                return Teens[random.Next(4)];
            }

            public string RandomAdult()
            {
                return Adults[random.Next(11)];
            }
            private void AddEggs()
            {
                Eggs = new Dictionary<int, string>();
                Eggs.Add(0, "Boy 1");
                Eggs.Add(1, "Boy 2");
                Eggs.Add(2, "Boy 3");
                Eggs.Add(3, "Boy 4");
                Eggs.Add(4, "Girl 1");
                Eggs.Add(5, "Girl 2");
                Eggs.Add(6, "Girl 3");
                Eggs.Add(7, "Girl 4");
            }

            private void AddBabies()
            {
                Babies= new Dictionary<int, string>();
                Babies.Add(0, "Babytchi");
                Babies.Add(1, "ShiroBabytchi");
            }

            private void AddToodlers()
            {
                Toodlers= new Dictionary<int, string>();
                Toodlers.Add(0, "Hashitamatchi");
                Toodlers.Add(1, "Marutchi");
                Toodlers.Add(2, "Tongaritchi");
                Toodlers.Add(3, "Tonmarutchi");
            }

            private void AddTeens()
            {
                Teens= new Dictionary<int, string>();
                Teens.Add(0, "Kutchitamatchi");
                Teens.Add(1, "Nyorotchi");
                Teens.Add(2, "Takotchi");
                Teens.Add(3, "Tamatchi");
            }

            private void AddAdults()
            {
                Adults= new Dictionary<int, string>();
                Adults.Add(0, "Bill");
                Adults.Add(1, "Ginjirotchi");
                Adults.Add(2, "Hashizotchi");
                Adults.Add(3, "Kuchipatchi");
                Adults.Add(4, "Kusatchi");
                Adults.Add(5, "Mametchi");
                Adults.Add(6, "Masukutchi");
                Adults.Add(7, "Mimitchi");
                Adults.Add(8, "Oyajitchi");
                Adults.Add(9, "Pochitchi");
                Adults.Add(10, "Zakutchi");
            }
        }
    }
}
