using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.design_pattern.p1
{
    public class Soldier : Character
    {
        public Soldier()
            : this(new AxeWeapon())
        {
        }

        public Soldier(IWeapon weapon)
            : base(weapon)
        {
        }

        public override void Fight()
        {
            Console.Write("Soldier Fight\t");
            base.Fight();
        }
    }
}
