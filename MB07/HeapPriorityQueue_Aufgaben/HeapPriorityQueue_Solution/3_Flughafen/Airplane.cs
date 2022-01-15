using System;

namespace _3_Flughafen {
    public class Airplane : IComparable {
        public Airplane(string departureAirport, int quantityOfPetrol) {
            this.DepartureAirport = departureAirport;
            this.QuantityOfPetrol = quantityOfPetrol;
        }

        public string DepartureAirport { get; }
        public int QuantityOfPetrol { get; }
        public int CompareTo(object obj) {
            var otherAirplane = obj as Airplane;
            if (otherAirplane == null) {
                return -1;
            }

            return this.QuantityOfPetrol.CompareTo(otherAirplane.QuantityOfPetrol);
        }

    }
}
