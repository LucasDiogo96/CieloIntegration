using System;
using System.Globalization;

namespace Domain.Entities
{
    public class CardExpiration
    {
         readonly byte _month;
         readonly short _year;

        public CardExpiration(short year, byte month)
        {
            if ((short)DateTime.Now.Year >= year && (byte)DateTime.Now.Month > month)
                throw new Exception("Card Expiration Date is invalid");

            _year = year;
            _month = month;
        }

        public override string ToString()
        {
            var monthAsString = _month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
            return string.Format("{0}/{1}", monthAsString, _year);
        }
    }
}
