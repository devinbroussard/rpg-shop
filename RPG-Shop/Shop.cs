using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Shop
{
    class Shop
    {
        private int _gold;
        private Item[] _inventory;

        public Item[] Inventory
        {
            get { return _inventory; }
        }
        public Shop(Item[] inventory)
        {
            _inventory = inventory;
        }

        public bool Sell(Player player, int i)
        {
            if (player.Gold >= _inventory[i].Cost)
            {
                _gold += _inventory[i].Cost;
                return true;
            }
            else
                return false;
        }

        public string[] GetItemNames()
        {
            string[] itemNames = new string[_inventory.Length];

            for (int i = 0; i < _inventory.Length; i++)
                itemNames[i] = $"{_inventory[i].Name} - {_inventory[i].Cost}g";

            return itemNames;
        }
    }
}
