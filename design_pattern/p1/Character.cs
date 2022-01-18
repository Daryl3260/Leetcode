using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.design_pattern.p1
{
    public class Character
    {
        public Character(IWeapon weapon)
        {
            this.Weapon = weapon;
        }

        public IWeapon Weapon { get; set; }

        public virtual void Fight()
        {
            this.Weapon.fight();
        }
    }
}
