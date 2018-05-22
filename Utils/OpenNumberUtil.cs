using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVC.Controllers;
using MVC.Tasks;

namespace MVC.Utils
{
    public static class OpenNumberUtil
    {
        private static List<int> _openNumberList;
        private static int _openNumberIndex = 0;
        private static Random _openNumberRandom = new Random();
        public static int Count { get; set; }

        internal static void CreateOpenNumbers(int count)
        {
            if (Count <= 0)
            {
                Count = count;
            }
            if (_openNumberList != null)
            {
                _openNumberList.Clear();
            }
            var maxOpenNumber = (int) Math.Pow(36, 4);
            var eachLength = maxOpenNumber / Count;
            var randArray = new int[Count];
            for (int i = 0; i < Count; i++)
            {
                randArray[i] = i;
            }
            for (int i = 0; i < Count; i++)
            {
                int index = _openNumberRandom.Next(0, Count);
                var temp = randArray[i];
                randArray[i] = randArray[index];
                randArray[index] = temp;
            }
            _openNumberIndex = 0;
            _openNumberList = new List<int>(randArray);
        }

        internal static int GetOpenNumber(this UserController controller)
        {
            if (_openNumberList != null && _openNumberList.Count > 0 && _openNumberIndex < _openNumberList.Count)
            {
                return _openNumberList[_openNumberIndex++];
            }
            return -1;
        }

        internal static bool UpdateOpenNumbers(this IntervalTask task)
        {
            if (_openNumberIndex >= _openNumberList.Count)
            {
                CreateOpenNumbers(Count);
                return true;
            }
            return false;
        }
    }
}