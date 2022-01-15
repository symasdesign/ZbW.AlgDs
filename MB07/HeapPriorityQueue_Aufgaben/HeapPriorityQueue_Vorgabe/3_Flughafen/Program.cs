using System;

namespace _3_Flughafen {
    static class Program {
        static void Main(string[] args) {
            var landingOrder = new LandingOrder();

            landingOrder.AddAirplane(new Airplane("Basel", 20));
            landingOrder.AddAirplane(new Airplane("Geneva", 100));
            landingOrder.AddAirplane(new Airplane("New-York", 10));
            landingOrder.AddAirplane(new Airplane("London", 5));
            landingOrder.AddAirplane(new Airplane("Tel Aviv", 300));

            Airplane nextLanding;
            while ((nextLanding = landingOrder.GetNextAirplane()) != null) {
                Console.WriteLine($"Airplane from {nextLanding.DepartureAirport} has landed.");
            }

            Console.WriteLine("All airplanes have landed.");
            Console.ReadKey();
        }

/* Session-Log:

Airplane from London has landed
Airplane from New-York has landed
Airplane from Basel has landed
Airplane from Geneva has landed
Airplane from Tel Aviv has landed
All airplanes have landed.

*/
    }
}
