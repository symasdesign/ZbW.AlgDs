using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace WebServer
{
    public class HttpServer
    {
        TcpListener listener = null;

        public int Port { get; private set; }
        public string LocalPath { get; private set; }

        public HttpServer(int port, string localPath)
        {
            Port = port;
            LocalPath = localPath;
        }

        public void Start()
        {
            listener = new TcpListener(IPAddress.Loopback, Port);

            listener.Start();

            var requests = new Requests();

            while (true)
            {
                var c = listener.AcceptTcpClient();

                requests.Add(c);

                if (!listener.Pending())
                    requests.Process(LocalPath);
            }
        }

        private class Requests
        {
            My.Collections.Queue<TcpClient> queue = new My.Collections.Queue<TcpClient>();

            public void Add(TcpClient c)
            {
                queue.Enqueue(c);
            }

            public void Process(string path)
            {
                Console.WriteLine("Queue: " + queue.Count);

                while (queue.Count > 0)
                {
                    try
                    {
                        var r = new Request(queue.Dequeue(), path);
                        Console.WriteLine("  " + r.LocalUrl);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                    }
                }
            }
        }

        private class Request
        {
            private TcpClient c;
            private string localPath;

            public string LocalUrl { get; private set; }
            public string Verb { get; private set; }
            public string HttpVersion { get; private set; }
            public List<string> Header { get; private set; }

            public Request(TcpClient c, string localPath)
            {
                this.c = c;
                this.localPath = localPath;

                Header = new List<string>();

                Process();
            }

            private void Process()
            {
                var r = new StreamReader(c.GetStream());    // Lesen vom Remote-Host
                var w = new StreamWriter(c.GetStream());    // Schreiben an Remote-Host

                try
                {
                    HandleRequest(r, w);
                }
                catch (Exception e)
                {
                    Failure(w);
                    throw e;
                }
                finally
                {
                    w.Close();
                    r.Close();
                    c.Close();
                }
            }

            private void HandleRequest(StreamReader r, StreamWriter w)
            {
                string request = r.ReadLine();

                if (request != null)
                {
                    string[] tokens = request.Split(' ');

                    Verb = tokens[0].ToUpper();
                    LocalUrl = tokens[1];
                    HttpVersion = tokens[2];

                    ReadHeader(r);

                    string file = Path.Combine(
                        Path.GetFullPath(localPath), LocalUrl.TrimStart('/'));

                    string text = File.ReadAllText(file);

                    Success(w, Path.GetExtension(LocalUrl).TrimStart('.'));

                    w.Write(text);
                }
            }

            private void ReadHeader(StreamReader r)
            {
                string attribute;

                while ((attribute = r.ReadLine()) != null)
                {
                    if (attribute.Equals(""))
                        break;
                    Header.Add(attribute);
                }
            }

            private void Success(StreamWriter stream, string ext)
            {
                stream.WriteLine("HTTP/1.0 200 OK");
                stream.WriteLine("Content-Type: text/" + ext);
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
