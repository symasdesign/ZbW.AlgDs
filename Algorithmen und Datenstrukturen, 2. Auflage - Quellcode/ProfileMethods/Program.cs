using System;
using System.Diagnostics;
using System.Reflection;
using My.Collections;

namespace ProfileMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            int  count = 0;
            bool argsCorrect = args.Length > 0 ? int.TryParse(args[0], out count) : false;

            if (!argsCorrect)
            {
                Console.WriteLine("Aufruf: ProfileMethods <AnzahlDurchläufe>\nBeispiel: ProfileMethods 10000");
                return;
            }

            var profiler = new Profiler(count);
            
            var arrayList = new ArrayList<int>();
            var linkedList = new LinkedList<int>();
            var arrayListSystem = new System.Collections.Generic.List<int>();
            var linkedListSystem = new System.Collections.Generic.LinkedList<int>();

            profiler.AddMethod(arrayList,        "Add",     true);
            profiler.AddMethod(arrayList,        "Remove",  true);
            profiler.AddMethod(linkedList,       "Add",     true);
            profiler.AddMethod(linkedList,       "Remove",  true);
            profiler.AddMethod(arrayListSystem,  "Add",     true);
            profiler.AddMethod(arrayListSystem,  "Remove",  true);
            profiler.AddMethod(linkedListSystem, "AddLast", true);
            profiler.AddMethod(linkedListSystem, "RemoveLast", true);
            profiler.AddMethod(arrayList,        "Add",     false);
            profiler.AddMethod(arrayList,        "RemoveAt", true);
            profiler.AddMethod(arrayListSystem,  "Add",     false);
            profiler.AddMethod(arrayListSystem,  "RemoveAt", true);

            //profiler.AddMethod(arrayList,        "Add",     false);
            //profiler.AddMethod(arrayList,        "IndexOf", true);
            //profiler.AddMethod(arrayListSystem,  "Add",     false);
            //profiler.AddMethod(arrayListSystem,  "IndexOf", true);
            //profiler.AddMethod(linkedList,       "Add",     false);
            //profiler.AddMethod(linkedList,       "Contains", true);
            //profiler.AddMethod(linkedListSystem, "AddLast",  false);
            //profiler.AddMethod(linkedListSystem, "Contains", true);

            profiler.Run();
            
        }

        private class Profiler
        {
            int count;
            int countData;  // Anzahl Messwerte  
            Stopwatch watch = new Stopwatch();

            ArrayList<Method> list = new ArrayList<Method>();

            private class Method
            {
                public Object Obj { get; set; }
                public string Name { get; set; }
                public bool Protocol { get; set; }
            }

            public Profiler(int count)
            {
                this.count = count;
                countData = count / 1000;

                Console.WriteLine("Methode;Anzahl;Zeit(Sek.);Zeit(Ticks);Zeit pro Element(ms)");
            }

            public void AddMethod(Object obj, string name, bool protocol)
            {
                list.Add(new Method() { Obj = obj, Name = name, Protocol = protocol });
            }

            public void Run()
            {
                for(int i = 0; i < list.Count; i++)
                {
                    var p = list[i];
                    var obj = p.Obj;

                    var type = obj.GetType();
                    var methods = type.GetMethods();
                    MethodInfo method = null;
                    
                    foreach(var methodInfo in methods)
                    {
                        if (methodInfo.Name.Equals(p.Name))
                        { 
                            method = methodInfo;
                            break;
                        }
                    }

                    if(method == null)
                    {
                        Console.WriteLine(type.ToString() + "." + p.Name + " existiert nicht!");
                        continue;
                    }
                    
                    var countParams = method.GetParameters().Length;

                    watch.Restart();

                    for (int n = count-1; n >= 0; n--)
                    {
                        method.Invoke(obj, countParams == 0 ? null : new object[] { n });
                        Show(type.Name + "." + method.Name, n, p.Protocol);
                    }

                    watch.Stop();
                }
            }

            private void Show(string method, int n, bool protocol)
            {
                if ((count-n) % countData == 0 && protocol == true) // alle countData Zeitausgabe
                    Console.WriteLine(method + ";" + 
                                     (count-n) + ";" + 
                                     (watch.ElapsedMilliseconds / 1000.0) + ";" +
                                     watch.ElapsedTicks + ";" +
                                     (double)watch.ElapsedMilliseconds / (count-n));
            }
        }
    }
}
