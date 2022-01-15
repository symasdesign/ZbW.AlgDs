using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinMaxHeap;

namespace _3_Flughafen {
    public class LandingOrder {
        MinHeap priorityQueue = new MinHeap();

        public void AddAirplane(Airplane airplane) {
            // TODO: implement
            //       Add Airplane to Priorityqueue
            this.priorityQueue.Add(airplane);
        }

        public Airplane GetNextAirplane() {
            // TODO: implement
            //       Get next Airplane (with least full) from Priorityqueue
            if (this.priorityQueue.Empty) {
                return null;
            }

            return (Airplane)this.priorityQueue.Pop();
        }
    }
}
