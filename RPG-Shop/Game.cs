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
        public int Cost;
        public int Name;
    }


    class Game
    {
        Player _player;
        Shop _shop;
        bool _gameOver;
        int _currentScene;

        public void Run()
        {
            Start();

            while (!_gameOver)
                Update();

            End();
        }

        void Start()
        {
            _player = new Player(30);
            _gameOver = false;
            _currentScene = 0;
        }

        void Update()
        {
            DisplayCurrentScene();
        }

        void End() { }

        void InitializeItems() { }

        int GetInput(string description, params string[] options)
        {
            return 0;
        }

        void Save() { }

        bool Load() { return true; }

        void DisplayCurrentScene() 
        {
            switch (_currentScene)
            {
                case 0:
                    DisplayOpeningMenu();
                    break;
            }
        }

        void DisplayOpeningMenu() 
        {
            Console.WriteLine("Welcome to RPG Shop!");
            Console.ReadKey(true);
            Console.Clear();
        }

        void GetShopMenuOptions() { }

        void DisplayShopMenu() { }
    }
}
