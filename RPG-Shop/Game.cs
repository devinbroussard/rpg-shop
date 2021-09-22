using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Shop
{
    /// <summary>
    /// Represents items
    /// </summary>
    struct Item
    {
        public string Name;
        public int Cost;
    }


    class Game
    {
        private Player _player = new Player(350);
        private Shop _shop;
        private Item[] _playerItems;
        private Item[] _shopItems;
        private bool _gameOver;
        private int _currentScene;

        public void Run()
        {
            Start();

            while (!_gameOver)
                Update();

            End();
        }

        private void Start()
        {
            _gameOver = false;
            _currentScene = 0;

            InitializeItems();
        }

        private void Update()
        {
            DisplayCurrentScene();
        }

        private void End()
        {
            Console.WriteLine("Goodbye, player!");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Function used to initialize game items
        /// </summary>
        private void InitializeItems()
        {
            //Initializing shop items
            Item shotgun = new Item { Name = "Shotgun for Lodis", Cost = 300 };
            Item eggs = new Item { Name = "Eggs for Lodis", Cost = 2 };
            Item smashBros = new Item { Name = "Copy of Smash for Lodis", Cost = 60 };

            _shopItems = new Item[] { shotgun, eggs, smashBros };
            _shop = new Shop(_shopItems);
        }

        private int GetInput(string description, params string[] options)
        {
            string input = "";
            int inputReceived = -1;

            while (inputReceived == -1)
            {
                Console.WriteLine($"{description}\n");

                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {options[i]}");
                }
                Console.Write("> ");

                //Get input from player
                input = Console.ReadLine();

                //If the player typed an int...
                if (int.TryParse(input, out inputReceived))
                {
                    //...decrement the input and check if it's within the bounds of the array
                    inputReceived--;

                    if (inputReceived < 0 || inputReceived >= options.Length)
                    {
                        //Set the input received to be the default value
                        inputReceived = -1;
                        //Display an error message
                        Console.WriteLine("Invalid input.");
                    }
                }
                else
                {
                    inputReceived = -1;
                    Console.WriteLine("Invalid input.");
                }

                Console.ReadKey(true);
                Console.Clear();
            }
            
            return inputReceived;
        }

        private void Save() { }

        private bool Load() { return true; }

        private void DisplayCurrentScene() 
        {
            switch (_currentScene)
            {
                case 0:
                    DisplayOpeningMenu();
                    break;
                case 1:
                    DisplayShopMenu();
                    break;
            }
        }

        private void DisplayOpeningMenu() 
        {
            int input = GetInput("Welcome to RPG Shop! Would you like to:", "Start new game", "Load existing game");

            if (input == 0)
            {
                _currentScene = 1;
            }

            else if (input == 1)
            {
                if (Load())
                {
                    Console.Clear();
                    Console.WriteLine("Load successful!");
                    Console.ReadKey(true);
                    Console.Clear();
                    _currentScene = 1;
                }
            }
        }

        private void GetShopMenuOptions() 
        {
        }

        private void DisplayShopMenu() 
        {
            Console.WriteLine($"Your gold: {_player.Gold}\n");
            Console.WriteLine("Your inventory:");
            Console.WriteLine(_player.GetItemNames());

            Console.ReadKey(true);
            Console.Clear();


        }
    }
}
