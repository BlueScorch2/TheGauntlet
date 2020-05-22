using System;
using System.Collections.Generic;
using System.Text;

namespace TheGauntlet.Overworld
{
    public static class ItemContainer
    {
        public static Item AttackBoost = new Item(name: "Attack Boost", onApply: (player) => {
            player.Attack += 2;
            Console.WriteLine("Attack increased by 2!");
        });

        public static Item DefenceBoost = new Item(name: "Defence Boost", onApply: (player) => {
            player.Defence += 2;
            Console.WriteLine("Defence increased by 2!");
        });

        public static Item HealthBoost = new Item(name: "Health Boost", onApply: (player) => {
            player.MaxHealth += 20;
            Console.WriteLine("Health increased by 20!");
        });

        public static Item SpeedBoost = new Item(name: "Speed Boost", onApply: (player) => {
            player.Speed += 2;
            Console.WriteLine("Speed increased by 2!");
        });
    }
}
