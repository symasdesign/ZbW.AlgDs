using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Aufgaben_Queue
{
    public class DatabaseServer
    {
        public int Port { get; private set; }
        public string Folder { get; private set; }

        public DatabaseServer(int port, string folder)
        {
            Port = port;
            Folder = folder;
        }

        public void Start()
        {
            IPAddress address = IPAddress.Parse("127.0.0.1");

            var listener = new TcpListener(address, Port);

            listener.Start();

            while (true)
            {
                var client = listener.AcceptTcpClient();
                var request = new Request(client, Folder);

                if (!listener.Pending())
                    request.Process();
            }
        }

        class Request
        {
            private TcpClient client;
            private string folder;

            public string LocalUrl { get; private set; }
            public string Verb { get; private set; }
            public string HttpVersion { get; private set; }

            public Request(TcpClient client, string folder)
            {
                this.client = client;
                this.folder = folder;
            }

            public void Process()
            {
                var reader = new StreamReader(client.GetStream());    // Lesen vom Remote-Host
                var writer = new StreamWriter(client.GetStream());    // Schreiben an Remote-Host

                try
                {
                    HandleREST(reader, writer);
                }
                catch (Exception e)
                {
                    Failure(writer);
                    throw e;
                }
                finally
                {
                    writer.Close();
                    reader.Close();
                    client.Close();
                }
            }

            private void HandleREST(StreamReader reader, StreamWriter writer)
            {
                string request = reader.ReadLine();

                if (request != null)
                {
                    string[] tokens = request.Split(' ');

                    Verb = tokens[0].ToUpper();
                    LocalUrl = tokens[1];
                    HttpVersion = tokens[2];

                    while (reader.ReadLine() != "")  // Header übergehen
                        ;

                    var parts = LocalUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 2)
                        return;

                    string table = Path.Combine(Path.GetFullPath(folder), "tablespace", parts[0] + ".csv");
                    string filter = parts[1];

                    var records = File.ReadAllLines(table);

                    Success(writer);

                    foreach (var record in records)
                    {
                        if (record.ToUpper().Contains(filter.ToUpper()))
                            writer.WriteLine(record);
                    }
                }
            }

            private void Success(StreamWriter stream)
            {
                stream.WriteLine("HTTP/1.0 200 OK");
                stream.WriteLine("Content-Type: text/txt");
                stream.WriteLine("Connection: close");
                stream.WriteLine("");
            }

            private void Failure(StreamWriter stream)
            {
                stream.WriteLine("HTTP/1.0 404 not found");
                stream.WriteLine("Connection: close");
                stream.WriteLine("");
            }
        }
    }
}