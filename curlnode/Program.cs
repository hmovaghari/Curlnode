using System;
using System.Diagnostics;
using System.Linq;

namespace curlnode
{
    internal class Program
    {
        private static bool? isSecure;

        static void Main(string[] args)
        {
            //args = new string[] { "https://hmovaghari.ir/root/ip.js" };
            if ((args?.Count() ?? 0) == 0 || args.Count() > 1)
            {
                Console.WriteLine("curlnode: try 'curlnode --help' for more information");
            }
            else if (args[0] == "--help")
            {
                Console.WriteLine("To run, node must be installed on the system");
                Console.WriteLine("curlnode: try 'curlnode http://example.com/file.js");
                Console.WriteLine("curlnode: try 'curlnode file:///C:/example/file.js'");
            }
            else
            {
                try
                {
                    var command = "/c curl -s -k " + args[0] + @"| node -e ""let data = ''; process.stdin.on('data', chunk => { data += chunk; }); process.stdin.on('end', () => { eval(data); }); """;
                    //Process.Start("CMD.exe", command);

                    Process process = new Process();
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.FileName = "CMD.exe";
                    process.StartInfo.Arguments = command;
                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    Console.WriteLine(output);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Error: {exception.Message}");
                }
            }
        }
    }
}
