using IFlatHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFlatHandler.Models
{
    public abstract class Flat : IRealEstate
    {
        protected double area;
        protected int roomsCount;
        protected int inhabitantsCount;
        protected int unitPrice;

        public int InhabitantsCount { get => inhabitantsCount; }

        public Flat(double area, int roomsCount, int inhabitantsCount, int unitPrice)
        {
            this.area = area;
            this.roomsCount = roomsCount;
            this.inhabitantsCount = inhabitantsCount;
            this.unitPrice = unitPrice;
        }

        /// <summary>
        /// ami embereket k¨olt¨oztet a
        /// lak´asba.A visszat´er´esi ´ert´ek att´ol f¨ugg, hogy sikeres volt-e a bek¨olt¨oz´es.
        /// </summary>
        /// <param name="newInhabitants">A beköltöztetendő lakosok száma</param> //azert genearlja ki mert van parametere
        /// <returns> A visszat´er´esi ´ert´ek att´ol f¨ugg, hogy sikeres volt-e a bek¨olt¨oz´es</returns> //azert genearlja ki mert van retrunje
        public abstract bool MoveIn(int newInhabitants);
        
        public int TotalValue()
        {
            return (int)Math.Round(area * unitPrice);
        }
        public override string ToString()
        {
            return String.Format("Area: {0}, RoomsCount: {1}, InhabitantsCount {2}, UnitPrice: {3}", area, roomsCount, inhabitantsCount, unitPrice);
        }
    }
}
