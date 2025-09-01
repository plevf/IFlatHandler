using IFlatHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFlatHandler.Models
{
    public class ApartmentHouse
    {
        const string GarageString = "Garazs";
        const string LodgingsString = "Alberlet";
        const string IsHeatedString = "futott";

        int flatCount;
        int garageCount;
        int maxFlatCount;
        int maxGarageCount;
        public Garage[] Garages { get; private set; }
        public Flat[] Flats { get; private set; }
        public int InhabitantsCount
        {
            get
            {
                int count = 0;
                foreach (Flat flat in Flats)
                {
                    count += flat.InhabitantsCount;
                }
                return count;
            }
        }
        public ApartmentHouse(int maxFlatCount, int maxGarageCount)
        {
            this.maxFlatCount = maxFlatCount;
            this.maxGarageCount = maxGarageCount;

            Flats = new Flat[maxFlatCount];
            Garages = new Garage[maxGarageCount]; //meghatarozza, hogy maga a tömb mekkora legyen (ez kesobb mar nem vltoztathato ertelemszeruen)
        }
        public bool AddFlatsGarages(IRealEstate realEstate) //itt arra hasznaljuk az interface-t, hogy az interface-t megvalosító objektumokat meg tudjuk adni igy parameterkent miatta és el is tudjuk donteni hogy milyen objektum
        {
            bool isGarage = realEstate is Garage; //azert nezzuk ezt mert ez van legalul a hierarchiaban
            if (isGarage && garageCount == maxGarageCount) return false;
            if (!isGarage && flatCount == maxFlatCount) return false;

            if (isGarage)
            {
                Garages[garageCount] = realEstate as Garage;
            }
            else
            {
                Flats[flatCount] = realEstate as Flat; //ez flat tipusokat tartalmaz, tehat lehet akar lodging vagy familyapartment is
            }
            return true;
        }
        public int TotalValue()
        {
            int count = 0;
            foreach (Flat flat in Flats)
            {
                if (flat.InhabitantsCount >= 1)
                {
                    count += flat.TotalValue();
                }
            }
            foreach (Garage garage in Garages)
            {
                if (garage.IsBooked)
                {
                    count += garage.TotalValue();
                }
                
            }
            return count;
        }
        public static ApartmentHouse LoadFromFile(string fileName)
        {
            int GarageCount = 0;
            int FlatCount = 0;

            StreamReader sr = new StreamReader(fileName);
            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine()!.Split(" "); //line.StartWith-el is csinalhatnam es akkor nem kene tombot csinalni és split se kene ebben a streamreaderben
                if (line[0] == GarageString)
                {
                    GarageCount++;
                }
                else
                {
                    FlatCount++;
                }
            }

            sr.Close();

            ApartmentHouse ap = new ApartmentHouse(FlatCount, GarageCount);

            sr = new StreamReader(fileName);

            CultureInfo prevCulture = CultureInfo.CurrentCulture; //kimentjük a jelenlegi cultre infot
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine()!.Split(" ");
                if (line[0] == LodgingsString)
                {
                    ap.AddFlatsGarages(new Lodgings(double.Parse(line[1]), int.Parse(line[2]), int.Parse(line[3])));
                }
                else if (line[0] == GarageString)
                {
                    ap.AddFlatsGarages(new Garage(double.Parse(line[1]), int.Parse(line[2]), HeatedAsString(line[3])));
                }
                else
                {
                    ap.AddFlatsGarages(new Lodgings(double.Parse(line[1]), int.Parse(line[2]), int.Parse(line[3])));
                }
            }

            CultureInfo currentCulture = prevCulture;

            sr.Close();

            return ap;
        }
        public static bool HeatedAsString(string isHeated)
        {
            if(isHeated == IsHeatedString)
            {
                return true;
            }
            else
            {
                return false;
            }

            //return rawHeated == IsHeatedString; --ez ugyanaz
        }
    }
}
