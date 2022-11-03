using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MusicDB
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("Aufruf: MusicDB <folder>");

            // Musikdateien einlesen...
            var discs = new Discs(args[0]);

            // ...oder vom Cache
            string fileDb = Path.Combine(args[0], "musicDB.bin");
            if (!File.Exists(fileDb))
            {
                discs.Load();
            }
            else
            {
                using (Stream stream = File.OpenRead(fileDb))
                {
                    var deserializer = new BinaryFormatter();
                    discs = (Discs)deserializer.Deserialize(stream);
                }
            }

            Console.Write("DiscId eingeben, z.B. 2d0bde04: ");

            string discId = "";
            while ((discId = Console.ReadLine()) != "")
            {
                if (string.IsNullOrEmpty(discId))
                    break;

                Disc title = discs.Read(discId);

                if (title == null)
                    Console.WriteLine("Titel nicht gefunden!");
                else
                    Console.WriteLine($"{title}\n");
            }

            using (FileStream stream = File.Create(fileDb))
            {
                var serializer = new BinaryFormatter();
                serializer.Serialize(stream, discs);
            }
        }
    }
}
