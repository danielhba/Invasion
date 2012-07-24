using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFramework
{
    public static class GameHelper
    {
        static Random _rand;

        public static int RandomNext(int maxValue)
        {
            return RandomNext(0, maxValue);
        }

        public static int RandomNext(int minValue, int maxValue)
        {
            if (_rand == null)
                _rand = new Random();
            return _rand.Next(minValue, maxValue);
        }

        public static float RandomNext(float maxValue)
        {
            return RandomNext(0.0f, maxValue);
        }

        public static float RandomNext(float minValue, float maxValue)
        {
            if (_rand == null)
                _rand = new Random();
            return (float)((_rand.NextDouble() * (maxValue - minValue)) + minValue);
        }
    }
}
