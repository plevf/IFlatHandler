using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFlatHandler.Models
{
    public class FamilyApartment : Flat
    {
        int childrenCount;

        public FamilyApartment(double area, int roomsCount, int unitPrice, int inhabitantsCount = 0, int childrenCount = 0) : base(area, roomsCount, inhabitantsCount, unitPrice)
        {
            this.childrenCount = childrenCount;
        }
        public bool ChildIsBorn()
        {
            if (inhabitantsCount >= 2)
            {
                inhabitantsCount++;
                childrenCount++;
                return true;
            }
            else return false;
        }
        public override bool MoveIn(int newInhabitants)
        {
            double tempCount = inhabitantsCount + newInhabitants + childrenCount * 0.5;
            if (tempCount<0 && tempCount / roomsCount <= 2 && area / tempCount >= 10)
            {
                return true;
            }
            else return false;
        }
        public override string ToString()
        {
            return $"{base.ToString()}, ChildrenCount: {childrenCount}";
        }
    }
}
