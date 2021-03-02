using System;
using System.Collections.Generic;
using System.Linq;
using Encurtador.Intefaces;

namespace Encurtador.Services
{
    public class UrlService : IUrlService
    {
        private readonly string _baseUrlChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private readonly int _numberOfCharsToSelect = 5;

        public string GenerateHash()
        {
            int maxNumber = _baseUrlChars.Length;

            var randomiztor = new Random();
            var numList = new List<int>();

            for (int i = 0; i < _numberOfCharsToSelect; i++)
                numList.Add(randomiztor.Next(maxNumber));

            return numList.Aggregate(string.Empty, (current, num) => current + _baseUrlChars.Substring(num, 1));
        }
    }
}