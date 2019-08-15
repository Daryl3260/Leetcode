using System;
using System.Collections.Generic;
namespace Leetcode.jzoffer.Chapter3
{
    namespace p1{
        public class Solution{

            public static void Test(){
                var solution = new Solution();
                Console.WriteLine(solution.Power(2,8));
            }
            public double Power(double num,int exponent){
                if(exponent<0)return 1.0 / PositivePower(num, exponent);
                return PositivePower(num, exponent);
            }
            public double PositivePower(double num,int exponent){
                if(exponent==0)return 1.0;
                if(exponent==1)return num;
                if(num==0||num==1)return num;
                var dict = buildDict(num, exponent);
                return SubPower(num, exponent, dict);
            }
            private Dictionary<int,double> buildDict(double num,int exponent){
                var dict = new Dictionary<int, double>();
                dict[0] = 1.0;
                dict[1] = num;
                var n = num;
                var exp = 1;
                while(exponent>(exp<<1)){
                    n *= n;
                    exp <<= 1;
                    dict[exp] = n;
                }
                return dict;
            }
            private double SubPower(double num,int exponent,Dictionary<int,double> dict){
                if(dict.ContainsKey(exponent))return dict[exponent];
                var max = 0;
                foreach(var key in dict.Keys){
                    if(key<exponent&&key>max){
                        max = key;
                    }
                }
                return SubPower(num, exponent - max, dict) * dict[max];
            }
        }
    }

    namespace p1.better
    {
        public class Solution
        {
            public static void Test()
            {
                var s1 = new p1.Solution();
                var s2 = new Solution();
                var num = 2;
                var exponent = 11;
                Console.WriteLine($"{s1.PositivePower(2,11)}");
                Console.WriteLine($"{s2.PositivePower(2,11)}");
            }
            public double Power(double num, int exponent)
            {
                if (exponent < 0) return PositivePower(num, -exponent);
                return PositivePower(num, exponent);
            }

            public double PositivePower(double num, int exponent)
            {
                if (exponent == 0) return 1.0;
                if (exponent == 1) return num;
                var rs = PositivePower(num, exponent >> 1);
                rs *= rs;
                if ((exponent & 0x1) == 1) rs *= num;
                return rs;
            }
        }
    }
}