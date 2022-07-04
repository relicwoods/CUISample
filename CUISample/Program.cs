// See https://aka.ms/new-console-template for more information

using System;
using McMaster.Extensions.CommandLineUtils;

namespace CUISample // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        => CommandLineApplication.Execute<Program>(args);

        [Option]



        private void OnExecute()
        {
            Console.WriteLine("Hello World!");
        }
    }
}




