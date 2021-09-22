using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RPG_Shop
{
    class Player
    {
        int _gold;
        Item[] _inventory;

        public int Gold
        {
            get { return _gold; }
        }

        public Player(int gold)
        {
            _gold = gold;
        }

        public void Buy(Item item)
        {
            Item[] newInventory = new Item[_inventory.Length + 1];

            for (int i = 0; i < _inventory.Length; i++)
                newInventory[i] = _inventory[i];

            newInventory[_inventory.Length] = item;

            _inventory = newInventory;
        }
        
        public string[] GetItemNames()
        {
            string[] itemNames = new string[_inventory.Length];

            for (int i = 0; i < _inventory.Length; i++)
                itemNames[i] = _inventory[i].Name;

            return itemNames;
        }

        public void Save(StreamWriter writer)
        {
            writer.WriteLine(_gold);
            writer.WriteLine(_inventory);
        }

        public bool Load(StreamReader reader)
        {
            if (int.TryParse(reader.ReadLine(), out _gold))
                return false;

            return true;
        }
    }
}
