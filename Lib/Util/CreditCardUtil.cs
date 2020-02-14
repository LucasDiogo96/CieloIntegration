using System;
using System.Text.RegularExpressions;

namespace Lib.Util
{
    public class CreditCardUtil
    {
        public static bool PassesLuhnTest(string cardNumber)
        {
            //Clean the card number- remove dashes and spaces
            cardNumber = cardNumber.Replace("-", "").Replace(" ", "");

            //Convert card number into digits array
            int[] digits = new int[cardNumber.Length];
            for (int len = 0; len < cardNumber.Length; len++)
            {
                digits[len] = Int32.Parse(cardNumber.Substring(len, 1));
            }

            //Luhn Algorithm
            //Adapted from code availabe on Wikipedia at
            //http://en.wikipedia.org/wiki/Luhn_algorithm
            int sum = 0;
            bool alt = false;
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int curDigit = digits[i];
                if (alt)
                {
                    curDigit *= 2;
                    if (curDigit > 9)
                    {
                        curDigit -= 9;
                    }
                }
                sum += curDigit;
                alt = !alt;
            }

            //If Mod 10 equals 0, the number is good and this will return true
            return sum % 10 == 0;
        }

        public static string GetBrand(string cardNumber)
        {
            if (Regex.Match(cardNumber, @"^4[0-9]{12}(?:[0-9]{3})?$").Success)
            {
                return CardBrand.Visa.ToString();
            }

            if (Regex.Match(cardNumber, @"^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$").Success)
            {
                return CardBrand.Master.ToString();
            }

            if (Regex.Match(cardNumber, @"^3[47][0-9]{13}$").Success)
            {
                return CardBrand.AmericanExpress.ToString();
            }

            if (Regex.Match(cardNumber, @"^6(?:011|5[0-9]{2})[0-9]{12}$").Success)
            {
                return CardBrand.Discover.ToString();
            }

            if (Regex.Match(cardNumber, @"^(?:2131|1800|35\d{3})\d{11}$").Success)
            {
                return CardBrand.JCB.ToString();
            }

            else
                return CardBrand.Visa.ToString();
        }

        public enum CardBrand
        {
            Visa,
            Master,
            Amex,
            AmericanExpress,
            Elo,
            Diners,
            Discover,
            JCB,
            Aura
        }
    }
}
