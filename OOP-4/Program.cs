using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace OOP_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const ConsoleKey PlayInMenu = ConsoleKey.D1;
            const ConsoleKey ExitInMenu = ConsoleKey.D2;

            Desk desk = new Desk();

            bool isWork = true;

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine($"Для начала игры нажмите : {PlayInMenu}");
                Console.WriteLine($"Для для выхода нажмите: {ExitInMenu}");

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

                switch ( consoleKeyInfo.Key )
                {
                    case ConsoleKey.D1:
                        desk.PlayGame();
                        break;

                    case ConsoleKey.D2:
                        isWork = false;
                        break;
                }
            }
        }
    }

    enum CardValue
    {
        six,
        seven,
        eight,
        nine,
        ten,
        jack,
        queen,
        king,
        ace
    }

    enum CardSuit
    {
        Spades,
        Hearts,
        Clubs,
        Diamonds
    }

    class Cards
    {
        public Cards(CardSuit suit, CardValue value)
        {
            Suit = suit;
            Value = value;
        }

        public CardSuit Suit { get; private set; }
        public CardValue Value { get; private set;}
    }

    class Desk
    {
        private List<Player> _players = new List<Player>();

        public void PlayGame()
        {
            Console.Clear();
            const ConsoleKey TakeCardInMenu = ConsoleKey.D1;
            const ConsoleKey ContinueInMenu = ConsoleKey.D2;

            int playerId = 0;

            AddPlayer(ref playerId);
            AddPlayer(ref playerId);

            foreach (Player player in _players)
            {
                bool isWork = true;

                while (isWork)
                {   
                    Console.Clear();
                    Console.WriteLine(player.Name);
                    Console.WriteLine($"Для взятия карты нажмите: {TakeCardInMenu}");
                    Console.WriteLine($"Для завершения набора: {ContinueInMenu}");
                    
                    ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

                    switch (consoleKeyInfo.Key)
                    {
                        case ConsoleKey.D1:
                            player.TakeCard();
                            player.ShowAllCards();
                            break;

                        case ConsoleKey.D2:                       
                            isWork = false;
                            break;

                        default:
                            Console.WriteLine("Ввод некоректен");
                            break;
                    }
                }
            }

            Console.Clear();

            foreach (Player player in _players)
            {
                Console.WriteLine();
                Console.WriteLine(player.Name);
                player.ShowAllCards();
            }            
        }
        private void AddPlayer(ref int playerId)
        {
            playerId++;
            string userInput;

            Console.Clear();
            Console.WriteLine($"Введите имя игрока номер: {playerId}");
            userInput = Console.ReadLine();

            _players.Add(new Player(userInput));
        }
    }

    class Player
    {
        private List<Cards> _cards = new List<Cards>();
        Random random = new Random();

        public Player(string name)
        {
            Name = name; 
        }

        public string Name { get; private set; }

        public void TakeCard()
        {
            int suit;
            int value;

            suit = random.Next(0, Enum.GetNames(typeof(CardSuit)).Length);
            value = random.Next(0, Enum.GetNames(typeof(CardValue)).Length);
            
            Cards card = new Cards((CardSuit)suit, (CardValue)value);

            _cards.Add(card);
        }

        public void ShowAllCards()
        {
            Console.WriteLine();

            foreach (Cards card in _cards)
            {
                Console.WriteLine(card.Suit + " " + card.Value);
            }

            Console.ReadKey();
        }
    }
}
