using System;
using System.IO;
using My.Collections;

namespace ZIPSearch
{
    public class ZIP
    {
        private Hashtable<int, Location> locations = new Hashtable<int, Location>();

        public ZIP(string file)
        {
            using (StreamReader r = new StreamReader(file))
            {
                // Spaltenüberschriften auslesen
                var cols = r.ReadLine().Split(new char[] { ';' });
                if (cols.Length != 3)
                    throw new ArgumentException("More than 3 columns in zipcode file");

                string line;
                while ((line = r.ReadLine()) != null)
                {
                    cols = line.Split(new char[] { ';' });

                    int zipcode = int.Parse(cols[0]);
                    if (!locations.ContainsKey(zipcode))
                    {
                        locations.Add(zipcode, new Location()
                        {
                            ZIP = int.Parse(cols[0]),
                            State = cols[1],
                            City = cols[2]
                        });
                    }
                }
            }
        }

        public Location SearchLocation(int zipcode)
        {
            if (locations.Contains(zipcode))
                return locations[zipcode];

            return null;
        }

        public class Location
        {
            public int ZIP { get; set; }
            public string State { get; set; }
            public string City { get; set; }

            public override string ToString()
            {
                return ZIP + " | " + State + " | " + City + "\n";
            }
        }
    }
}
