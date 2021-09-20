using System;
using System.Collections.Generic;
using System.Text;

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
        public Item[] Inventory
        {
            get { return _inventory; }
        }

        public Player(int gold)
        {
            _gold = gold;
        }
    }
}
