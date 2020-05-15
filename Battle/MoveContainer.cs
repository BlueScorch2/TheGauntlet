﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TheGauntlet.Battle
{
    public static class MoveContainer
    {
        public static Move Punch = new Move(
            name: "Punch", 
            description: "Throws a punch at the target.", 
            power: 30, 
            accuracy: 90, 
            specialEffect: null);

        public static Move BodySlam = new Move(
            name: "Body Slam",
            description: "Slams into the target.",
            power: 60, 
            accuracy: 60, 
            specialEffect: null);

        public static Move Toughen = new Move(
            name: "Toughen",
            description: "Increases defence.",
            power: 0, 
            accuracy: 100, 
            specialEffect: (user, target, damage) =>
            {
                user.Defence += 2;
            });

        public static Move UltimateSlam = new Move(
            name: "Ultimate Slam",
            description: "Charges into the target, stunning them.",
            power: 70, 
            accuracy: 60,
            specialEffect: (user, target, damage) =>
            {
                target.StatusEffect = StatusEffect.Stun;
                Console.WriteLine($"{target.Name} was stunned!");
            });

        public static Move Shinobi = new Move(
            name: "Shinobi",
            description: "Uses an ancient technique to poison the target.",
            power: 25,
            accuracy: 60,
            specialEffect: (user, target, damage) =>
            {
                target.StatusEffect = StatusEffect.Poison;
                Console.WriteLine($"{target.Name} was poisoned!");
            });

        public static Move LifeDrain = new Move(
            name: "Life Drain",
            description: "Steals the target's health.",
            power: 40,
            accuracy: 60,
            specialEffect: (user, target, damage) =>
            {
                user.Health += damage / 2;
                Console.WriteLine($"{user.Name} stole {damage / 2} health!");
            });

        public static Move Fireball = new Move(
            name: "Fireball",
            description: "Burns the target.",
            power: 40,
            accuracy: 60,
            specialEffect: (user, target, damage) =>
            {
                target.StatusEffect = StatusEffect.Burn;
                Console.WriteLine($"{target.Name} was burned!");
            });
    }
}