# Review 

The CMDParser library is based on classic builder pattern. This pattern is fitting for this problem and this library has implemented it well in most aspects. 

The project lacks any documentation whatsoever. There's no clearly distinguished file that the user should look into, when trying to learn the library. I ended up broswing through everything in the project. Finally, I discovered that example usage is written in "Tests". Definition of CLI was hidden in regions called "Helper classes" and  "Helper methods" which seems a little confusing (I wouldn't consider CLI definition a helper, even in context of the test). Overall, this wasn't too difficult to find due to the small size of the project, but it would have been, if the project was bigger. If this was my first experience with a library, I'd have passed onto next one.

During implementation of Numactl, I discovered that the library itself is easily extendible thanks to RegisterParser method. I also like the way SetupOption method is designed. It does lack build-time check for correctess of the CLI definition, but the trade-off is worth it. Passing a setter to the builder is a good design choice, however, calling this method "callback" is not. It's very unclear what the "callback" is for. You can't guess it's use without reading the example or documentation. I took me a minute to figure out the fact that I have to cerate an instance and then pass it into CreateParse(). It's not very intuitive and I don't see any reason why the caller is creating the instace, rather that the parser. 

Features of CMDParser were sufficient for the task I was given. However, native support for multiple command line formats (i.e. sets of options that can be used together) would result in more readable code. I like that the help interface allows to get description of each option separately and therefore allows developers to create their own help statements. 

Overall, I believe that the CMDParser library is well designed, with a few inconveniences that can be resolved in the next version.


# Example application

```
using CMDParser;
using CMDParser.Builders;
using System;
using System.Collections.Generic;
using System.Text;

using static CMDParser.OptionFactory;

namespace Numactl
{
    class CliParams
    {
        public class Node { }
        public class Cpu { }
        public class SizeInBytes {  }

        // numactl [ --interleave nodes ] [ --preferred node ] [ --membind nodes ]
        //         [ --cpunodebind nodes ] [ --physcpubind cpus ] [ --localalloc ] 
        //         [--] command {arguments ...}

        public Node[] InterleaveNodes { get; set; }
        public Node PrefferedNode { get; set; }
        public Node[] MembindNodes { get; set; }
        public Node[] CpuNodeBindNodes { get; set; }
        public Cpu[] PhysCpuBindCpus { get; set; }
        public bool IsLocalalloc { get; set; }
        public string Command { get; set; }
        public string[] Arguments { get; set; }

        // numactl --show
        public bool IsShow { get; set; }

        // numactl --hardware
        public bool IsHardware { get; set; }

        // numactl [ --huge ] [ --offset offset ] [ --shmmode shmmode ] [ --length length ] 
        // [ --strict ] [ --shmid id ] --shm shmkeyfile | --file tmpfsfile [ --touch ]
        // [ --dump ] [ --dump-nodes ] memory policy
        public bool IsHuge { get; set; }
        public SizeInBytes OffsetBytes { get; set; }
        public bool IsShmMode { get; set; }
        public SizeInBytes LengthBytes { get; set; }
        public bool IsStrict { get; set; }
        public int ShmId { get; set; }
        public string Shm { get; set; }
        public string File { get; set; }
        public bool IsTouch { get; set; }
        public bool IsDump { get; set; }
        public bool IsDumpNodes { get; set; }
        public string MemoryPolicy { get; set; }

    }

    class CliParserBuilder
    {
        public static ICommandLineParser BuildParser(CliParams cliParams)
        {
            var parserBuilder = new CommandLineParserBuilder();

            // You can register parse methods for whatever type you want to. Some of them are already preregistred.
            // parserBuilder.RegisterParser<ValueTuple<int, string, double>>(x => (1, x, 3.0));

            parserBuilder.RegisterParser<CliParams.Node>(x => throw new NotImplementedException());
            parserBuilder.RegisterParser<CliParams.Node[]>(x => throw new NotImplementedException());
            parserBuilder.RegisterParser<CliParams.Cpu>(x => throw new NotImplementedException());
            parserBuilder.RegisterParser<CliParams.SizeInBytes>(x => throw new NotImplementedException());

            // CLI Format 1
            parserBuilder
                .SetupOption<CliParams.Node[]>(Long("interleave"), Short('i'))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.InterleaveNodes = arg);

            parserBuilder
                .SetupOption<CliParams.Node>(Long("preferred"))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.PrefferedNode = arg);

            parserBuilder
                .SetupOption<CliParams.Node[]>(Long("membind"), Short('m'))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.MembindNodes = arg);

            parserBuilder
                .SetupOption<CliParams.Node[]>(Long("cpunodebind"), Short('N'))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.CpuNodeBindNodes = arg);

            parserBuilder
                .SetupOption<CliParams.Cpu[]>(Long("physcpubind"), Short('C'))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.PhysCpuBindCpus = arg);

            parserBuilder
                .SetupOption<bool>(Long("localalloc"), Short('l'))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.IsLocalalloc = arg);


            // CLI Format 2
            parserBuilder
                .SetupOption<bool>(Long("show"), Short('s'))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.IsShow = arg);

            // CLI Format 3
            parserBuilder
                .SetupOption<bool>(Long("hardware"), Short('H'))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.IsHardware = arg);

            // CLI Format 4
            parserBuilder
                .SetupOption<bool>(Long("huge"))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.IsHuge = arg);

            parserBuilder
                .SetupOption<CliParams.SizeInBytes>(Long("offset"))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.OffsetBytes = arg);

            parserBuilder
                .SetupOption<bool>(Long("shmmode"))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.IsShmMode = arg);

            parserBuilder
                .SetupOption<CliParams.SizeInBytes>(Long("length"))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.LengthBytes = arg);

            parserBuilder
                .SetupOption<bool>(Long("strict"))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.IsStrict = arg);

            parserBuilder
                .SetupOption<int>(Long("shmid"))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.ShmId = arg);

            parserBuilder
                .SetupOption<string>(Long("shm"))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.Shm = arg);

            parserBuilder
                .SetupOption<string>(Long("file"))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.File = arg);

            parserBuilder
                .SetupOption<bool>(Long("touch"))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.IsTouch = arg);

            parserBuilder
                .SetupOption<bool>(Long("dump"))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.IsDump = arg);

            parserBuilder
                .SetupOption<bool>(Long("dump-nodes"))
                .WithDescription("/some info/")
                .Callback(arg => cliParams.IsDumpNodes = arg);

            return parserBuilder.CreateParser();
        }
    }

    class Numactl
    {
        public void Main(string[] args)
        {
            var cliParams = new CliParams();
            var parser = CliParserBuilder.BuildParser(cliParams);
            parser.Parse(args);

            if(args.Length == 0)
            {
                Console.WriteLine(parser.Help);
            }
            else if(cliParams.IsShow)
            {
                Console.WriteLine("policy: default");
            } else if(cliParams.IsHardware)
            {
                Console.WriteLine("available: 2 nodes (0-1)");
            } else if(cliParams.Shm == null && cliParams.File == null)
            {
                Console.WriteLine("Running numactl (CLI FORMAT 1)");
                // do stuff
            } else 
            {
                Console.WriteLine("Running numactl (CLI FORMAT 4)");
                // do stuff
            }

            Console.WriteLine("Received the following parameters:");
            Console.WriteLine("{0}: {1}", nameof(cliParams.InterleaveNodes), cliParams.InterleaveNodes);
            Console.WriteLine("{0}: {1}", nameof(cliParams.PrefferedNode), cliParams.PrefferedNode);
            Console.WriteLine("{0}: {1}", nameof(cliParams.MembindNodes), cliParams.MembindNodes);
            Console.WriteLine("{0}: {1}", nameof(cliParams.CpuNodeBindNodes), cliParams.CpuNodeBindNodes);
            Console.WriteLine("{0}: {1}", nameof(cliParams.PhysCpuBindCpus), cliParams.PhysCpuBindCpus);
            Console.WriteLine("{0}: {1}", nameof(cliParams.IsLocalalloc), cliParams.IsLocalalloc);
            Console.WriteLine("{0}: {1}", nameof(cliParams.Command), cliParams.Command);
            Console.WriteLine("{0}: {1}", nameof(cliParams.Arguments), cliParams.Arguments);
            Console.WriteLine("{0}: {1}", nameof(cliParams.IsHuge), cliParams.IsHuge);
            Console.WriteLine("{0}: {1}", nameof(cliParams.OffsetBytes), cliParams.OffsetBytes);
            Console.WriteLine("{0}: {1}", nameof(cliParams.IsShmMode), cliParams.IsShmMode);
            Console.WriteLine("{0}: {1}", nameof(cliParams.LengthBytes), cliParams.LengthBytes);
            Console.WriteLine("{0}: {1}", nameof(cliParams.IsStrict), cliParams.IsStrict);
            Console.WriteLine("{0}: {1}", nameof(cliParams.ShmId), cliParams.ShmId);
            Console.WriteLine("{0}: {1}", nameof(cliParams.Shm), cliParams.Shm);
            Console.WriteLine("{0}: {1}", nameof(cliParams.File), cliParams.File);
            Console.WriteLine("{0}: {1}", nameof(cliParams.IsTouch), cliParams.IsTouch);
            Console.WriteLine("{0}: {1}", nameof(cliParams.IsDump), cliParams.IsDump);
            Console.WriteLine("{0}: {1}", nameof(cliParams.IsDumpNodes), cliParams.IsDumpNodes);
            Console.WriteLine("{0}: {1}", nameof(cliParams.MemoryPolicy), cliParams.MemoryPolicy);
        }
    }
}
```