using System;
using Random = UnityEngine.Random;

namespace Tetris.Board
{
    public static class ItemColorHelper
    {
        private static readonly ItemColor[] ItemColors;
        
        static ItemColorHelper()
        {
            ItemColors = (ItemColor[])Enum.GetValues(typeof(ItemColor));
        }
        
        public static ItemColor GetRandomItemColor()
        {
            return ItemColors[Random.Range(0, ItemColors.Length)];
        }
    }
}