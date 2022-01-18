using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.design_pattern.p1
{
    public class DaggerWeapon : IWeapon
    {
        public void fight()
        {
            System.Console.WriteLine("Fight with dagger");
        }
    }
}
