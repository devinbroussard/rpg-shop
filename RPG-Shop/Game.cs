using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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

            _shop = new Shop(shotgun, eggs, smashBros);
        }

        private int GetInput(string description, params string[] options)
        {
            string input = "";
            int inputReceived = -1;

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
                        Console.WriteLine("\nInvalid input.");
                        Console.ReadKey(true);
                    }
                }
                else
                {
                    inputReceived = -1;
                    Console.WriteLine("\nInvalid input.");
                    Console.ReadKey(true);
                }

                Console.Clear();

            return inputReceived;
        }

        private void Save()
        {
            StreamWriter writer = new StreamWriter("Inventory.txt");

            _player.Save(writer);
            _shop.Save(writer);

            writer.Close();
        }

        private bool Load() 
        {
            StreamReader reader = new StreamReader("Inventory.txt");

            if (!_player.Load(reader))
            {
                reader.Close();
                return false;
            }
            if (!_shop.Load(reader))
            {
                reader.Close();
                return false;
            }

            return true;
        }

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
                Load();
            }
        }

        private string[] GetShopMenuOptions() 
        {
            string[] shopItems = _shop.GetItemNames();
            string[] menuOptions = new string[shopItems.Length + 2];

            for (int i = 0; i < shopItems.Length; i++)
            {
                menuOptions[i] = shopItems[i];
            }

            menuOptions[shopItems.Length] = "Save Game";
            menuOptions[shopItems.Length + 1] = "Quit Game";

            return menuOptions;
        }

        private void DisplayShopMenu() 
        {
                string[] playerItemNames = _player.GetItemNames();

                Console.WriteLine($"Your gold: {_player.Gold}\n");
                Console.WriteLine("Your inventory:");
                foreach(string itemName in playerItemNames)
                {
                    Console.WriteLine(itemName);
                }
                Console.WriteLine();

                int inputReceived = GetInput("What would you like to purchase?", GetShopMenuOptions());

            if (inputReceived >= 0 && inputReceived < GetShopMenuOptions().Length - 2)
            {
                if (_shop.Sell(_player, inputReceived))
                {
                    Console.Clear();
                    Console.WriteLine($"You purchased the {_shop.GetItemNames()[inputReceived]}!");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You don't have enough gold for that.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }

            if (inputReceived == GetShopMenuOptions().Length - 2)
            {
                Console.Clear();
                Save();

                Console.WriteLine("Game saved successfully!");
                Console.ReadKey(true);
                Console.Clear();

            }
            if (inputReceived == GetShopMenuOptions().Length - 1)
            {
                _gameOver = true;
            }



        }
    }
}
