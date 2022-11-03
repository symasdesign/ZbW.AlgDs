using System;
using System.IO;
using System.Text;

namespace DublettenSoundex
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("Aufruf: DublettenSoundex <file.csv>");

            // CSV einlesen
            var duplicates = new DuplicateRecognitionSoundex(new StreamReader(args[0], Encoding.Default), 10);
            
            duplicates.Recognize();

            foreach (var d in duplicates.Duplicates)
                Console.WriteLine(d);
        }
    }
}
