using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.design_pattern.p1
{
    public class Rogue : Character
    {
        public Rogue(IWeapon weapon) : base(weapon) { }

        public Rogue() : this(new DaggerWeapon()) { }

        public override void Fight()
        {
            Console.Write("Rogue Fight\t");
            base.Fight();
        }
    }
}
