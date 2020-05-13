using CMDParser;
using CMDParser.Builders;
using System;
using System.Collections.Generic;
using static CMDParser.OptionFactory;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            NumaCtl.runWithArgs(args);
        }
    }

    class NumaCtl
    {
        class NumactlCommandLineArgs
        {
            public bool HardwareConf { get; set; }

            public bool ShowPolicy { get; set; }

            public string Interleave { get; set; }

            public string Preferrred { get; set; }

            public string MemBind { get; set; }

            public string PhysCPUBind { get; set; }
        }

        public NumaCtl()
        {
            // Init NUMActl
        }

        private static ICommandLineParser CreateNumaCtlCommandParser(NumactlCommandLineArgs output)
        {
            var parserBuilder = new CommandLineParserBuilder();

            parserBuilder.SetupOption<string>(Short('i'), Long("interleave"))
                .WithDescription("Interleave memory allocation across given nodes.")
                .Callback(nodes => output.Interleave = nodes);

            parserBuilder.SetupOption<string>(Short('p'), Long("preferred"))
                .WithDescription("Prefer memory allocations from given node.")
                .Callback(nodes => output.Preferrred = nodes);

            parserBuilder.SetupOption<string>(Short('m'), Long("membind"))
                .WithDescription("Allocate memory from given nodes only.")
                .Callback(nodes => output.MemBind = nodes);

            parserBuilder.SetupOption<string>(Short('C'), Long("physcpubind"))
                .WithDescription("Run on given CPUs only.")
                .Callback(cpus => output.PhysCPUBind = cpus);

            parserBuilder.SetupOption(Short('S'), Long("show"))
                .WithDescription("Show current NUMA policy.")
                .Callback(() => output.ShowPolicy = true);

            parserBuilder.SetupOption(Short('H'), Long("hardware"))
                .WithDescription("Print hardware configuration.")
                .Callback(() => output.HardwareConf = true);

            return parserBuilder.CreateParser();
        }

        public static void runWithArgs(String[] args)
        {
            // Arrange.
            var output = new NumactlCommandLineArgs();
            var parser = CreateNumaCtlCommandParser(output);

            // Act.
            var parsedArgs = parser.Parse(args);

            NumaCtl numaCtl = new NumaCtl();

            // Hardware configuration
            if (output.HardwareConf)
            {
                numaCtl.ShowHardwareConfiguration();
                return;
            }

            // Hardware policy
            if (output.ShowPolicy)
            {
                numaCtl.ShowPolicy();
                return;
            }

            // Conflicting options
            if ((output.Interleave != null && output.MemBind != null)
                || (output.Interleave != null && output.Preferrred != null)
                || (output.Preferrred != null && output.MemBind != null))
            {
                Console.WriteLine("Conflicting options, shutting off program!");
                return;
            }

            string command;
            // At least one command is present
            if (parsedArgs.Count == 0)
            {
                Console.WriteLine("There is no command present, shutting off program!");
                return;
            }
            else
            {
                command = parsedArgs[0];
            }

            // CPUs
            if (output.PhysCPUBind != null)
            {
                List<int> cpus = CreateList(output.PhysCPUBind, 31);
                if (cpus == null)
                {
                    Console.WriteLine("PhysCPUBind - only list of numbers or 'all' is accepted. ");
                }
                else
                {
                    numaCtl.setCPUs(cpus);
                }
            }

            // NUMA Policy
            // MemBind
            if (output.MemBind != null)
            {
                List<int> memList = CreateList(output.MemBind, 3);
                if (memList == null)
                {
                    Console.WriteLine("MemBind - only list of numbers or 'all' is accepted. ");
                }
                else
                {
                    Console.WriteLine("Running command:" + command);
                    numaCtl.setMemoryBind(memList);
                }
            }
            // Prefered
            if (output.Preferrred != null)
            {
                List<int> preferedList = CreateList(output.Preferrred, 3);
                if (preferedList == null)
                {
                    Console.WriteLine("Preferrred - only list of numbers or 'all' is accepted. ");
                }
                else
                {
                    Console.WriteLine("Running command:" + command);
                    numaCtl.setPreffered(preferedList);
                }
            }
            // Interleaved
            if (output.Interleave != null)
            {
                List<int> interleavedList = CreateList(output.Preferrred, 3);
                if (interleavedList == null)
                {
                    Console.WriteLine("Preferrred - only list of numbers or 'all' is accepted. ");
                }
                else
                {
                    Console.WriteLine("Running command:" + command);
                    numaCtl.setInterleaved(interleavedList);
                }
            }
        }

        private void setMemoryBind(List<int> memList)
        {
            printNodes("Memory binding:", memList);
        }

        private void setPreffered(List<int> preferedList)
        {
            printNodes("Preffered nodes:", preferedList);
        }

        private void setInterleaved(List<int> interleavedList)
        {
            printNodes("Interleaved nodes:", interleavedList);
        }

        private void setCPUs(List<int> cpus)
        {
            printNodes("CPU node bind:", cpus);
        }

        private void printNodes(string pre, List<int> nodes)
        {
            Console.Write(pre);
            for (int i = 0; i < nodes.Count; i++)
            {
                Console.Write(nodes[i]);
                if (i < nodes.Count - 1)
                {
                    Console.Write(',');
                }
            }
        }

        private void ShowHardwareConfiguration()
        {
            Console.WriteLine("available: 2 nodes(0 - 1)\n" +
                            "node 0 cpus: 0 2 4 6 8 10 12 14 16 18 20 22\n" +
                            "node 0 size: 24189 MB\n" +
                            "node 0 free: 18796 MB\n" +
                            "node 1 cpus: 1 3 5 7 9 11 13 15 17 19 21 23\n" +
                            "node 1 size: 24088 MB\n" +
                            "node 1 free: 16810 MB\n" +
                            "node distances:\n" +
                            "node   0   1 \n" +
                            "  0:  10  20 \n" +
                            "  1:  20  10 ");
        }
        private void ShowPolicy()
        {
            Console.WriteLine("available: 2 nodes (0-1)\n" +
                           "node 0 cpus: 0 2 4 6 8 10 12 14 16 18 20 22\n" +
                           "node 0 size: 24189 MB\n" +
                           "node 0 free: 18796 MB\n" +
                           "node 1 cpus: 1 3 5 7 9 11 13 15 17 19 21 23\n" +
                           "node 1 size: 24088 MB\n" +
                           "node 1 free: 16810 MB\n" +
                           "node distances:\n" +
                           "node   0   1 \n" +
                           "  0:  10  20 \n" +
                           "  1:  20  10 ");
        }

        static private List<int> CreateList(string s, int max)
        {
            List<int> ret = new List<int>();

            if (s == "all")
            {
                for (int i = 0; i <= max; i++)
                {
                    ret.Add(i);
                }
            }
            // List of ints
            else
            {
                string[] split = s.Split(',', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < split.Length; i++)
                {
                    int num;
                    if (int.TryParse(split[i], out num))
                    {
                        if (num <= max)
                        {
                            ret.Add(num);
                        }
                        else
                        {
                            // Out of list range
                            return null;
                        }
                    }
                    else
                    {
                        // There are not only numbers in list
                        return null;
                    }
                }
            }
            return ret;
        }
    }
}

/*
    1. General
    I like approach of defining own data container, where I can define types of fields.
    Approach of parser builder comes with some consequences. 
    For example defining conflicting options can be problematic

    I miss some examples in README.md showing supported argument types.
    Library is overall well documented.
        
    2. Numaclt example
    Writing of numactl example was straightforward.
    First I created structure for saving result of parser. Then create parser using builder, which is provided by library. 
    There is needed to define types of arguments.

    In user code I work with  following types: MyDataContainer, ICommandLineParser, ReadOnlyList<string>
    MyDataContainer is user for extraction of defined arguments.

    There is no support for definition of conflicting options. 
    This drawback I had to overcome by manualy writing down all possibilities which conflict.
    
    3. Checklist
    Setting of mandatory options is very easy, only calling ParameterRequired method during building option.
    Plain arguments are passed as readonly list.
    Synonyms are define by builder. Builder accepts array of lond or short options which create a synonym. 
    This approach is user-friendly.

    I havent found in documentation, when arguments are checked. But I think its when method "Parse" is called.
    I haven't found, how can I define bounded integers, enums and set of strings.
    There is a way on user side. Because approach of callback is very powerful.
    
    I like support for help. I can add description to every option, which is then showed.
    Library creates help string. The display of help is on user. This approach is good for testing, but it is not so user-friendly.
*/
