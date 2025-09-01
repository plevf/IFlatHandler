using IFlatHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFlatHandler.Models
{
    public class Lodgings : Flat, IRent
    {
        int bookedMonths;

        public bool IsBooked
        {
            get
            {
                if (bookedMonths == 0)
                    return false;
                else return true;
            }
        }

        public Lodgings(double area, int roomsCount, int unitPrice, int bookedMonths = 0, int inhabitantsCount=0) : base(area, roomsCount, inhabitantsCount, unitPrice)
        {
            this.bookedMonths = bookedMonths;
        }
        public bool Book(int months)
        {
            if (IsBooked)
                return false;
            bookedMonths = months;
            return true;
        }

        public int GetCost(int months)
        {
            if (inhabitantsCount != 0)
                return (int)Math.Round(TotalValue() / 240.0 / inhabitantsCount*months); // ha double lehet a muvelet eredmenye akkor az egyiket double-é kell tenni
            return 0;
        }

        public override bool MoveIn(int newInhabitants)
        {
            inhabitantsCount += newInhabitants;
            if(roomsCount != 0 && inhabitantsCount != 0 && IsBooked == true && inhabitantsCount/roomsCount<=8 && area / inhabitantsCount >= 2)
            {
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"{base.ToString()}, BookedMonths: {bookedMonths}";
        }
    }
}
