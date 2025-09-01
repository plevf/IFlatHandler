using IFlatHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace IFlatHandler.Models
{
    public class Garage : IRent, IRealEstate
    {
        double area;
        int unitPrice;
        bool isHeated;
        int months;
        bool isOccupied;

        public bool IsBooked => months > 0 || isOccupied; //OTF on the fly getter

        public Garage(double area, int unitPrice, bool isHeated, int months = 0, bool isOccupied = false)
        {
            this.area = area;
            this.unitPrice = unitPrice;
            this.isHeated = isHeated;
            this.months = months;
            this.isOccupied = isOccupied;
        }

        public bool Book(int months)
        {
            if (IsBooked)
                return false;
            this.months = months;
            return true;
        }

        public int GetCost(int months)
        {
            if (isHeated)
            {
                return (int)Math.Round(unitPrice / 120 * months * 1.5);
            }
            else return (int)Math.Round(unitPrice / 120.0 * months);
        }

        public int TotalValue()
        {
            return (int)Math.Round(area * unitPrice);
        }
        public void UpdateOccupied()
        {
            isOccupied = !isOccupied;
        }
        public override string ToString()
        {
            return String.Format("Area: {0}, UnitPrice: {1}, IsHeated: {2}, Months: {3}, IsOccupied: {4}", area, unitPrice, isHeated, months, isOccupied);
        }
    }
}
