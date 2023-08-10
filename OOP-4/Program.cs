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

    class Cards
    {
        public Cards(string suit, string value)
        {
            Suit = suit;
            Value = value;
        }

        public string Suit { get; private set; }
        public string Value { get; private set;}
    }

    class Desk
    {
        Random random = new Random();

        private List<Player> _players = new List<Player>();
        private List<Cards> _cards = new List<Cards>();

        public void PlayGame()
        {
            Console.Clear();
            const ConsoleKey TakeCardInMenu = ConsoleKey.D1;
            const ConsoleKey ContinueInMenu = ConsoleKey.D2;

            int playerId = 0;

            AddPlayer(ref playerId);
            AddPlayer(ref playerId);
            
            CreateCards();

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
                            GiveCardToPlayer(player);
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

        private void GiveCardToPlayer(Player player)
        {
            int cardId = random.Next(0, _cards.Count);
            player.TakeCard(_cards[cardId].Suit, _cards[cardId].Value);
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

        private void AddCard(string suit, string value)
        {
            _cards.Add(new Cards(suit,value));
        }

        private void CreateCards()
        {
            string[] _cardSuit = { "Spades", "Hearts", "Clubs", "Diamonds" };
            string[] _cardValue = { "six", "seven", "eight", "nine", "ten", "jack", "queen", "king", "ace" };

            for (int i = 0; i < _cardSuit.Length; i++)
            {
                for (int j = 0; j < _cardValue.Length ; j++)
                {
                    _cards.Add(new Cards(_cardSuit[i], _cardValue[j]));
                }
            }
        }
    }

    class Player
    {
        private List<Cards> _playerCards = new List<Cards>();

        public Player(string name)
        {
            Name = name; 
        }

        public string Name { get; private set; }

        public void TakeCard(string suit, string value)
        {
           _playerCards.Add(new Cards(suit,value));
        }

        public void ShowAllCards()
        {
            Console.WriteLine();

            foreach (Cards card in _playerCards)
            {
                Console.WriteLine(card.Suit + " " + card.Value);
            }

            Console.ReadKey();
        }
    }
}
