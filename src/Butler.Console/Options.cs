using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Butler.Console
{
    public class Options
    {
        [Option('t', "target", Required = true, HelpText = "管理目标 asset 资产 data 数据")]
        public string Target { get; set; }

        [Option("command", Required = true, HelpText = "指令，详细指令说明请参照 Readme.md")]
        public string Command { get; set; }

        [Option('f', "fundcode")]
        public string FundCode { get; set; }

        [Option("cost", HelpText = "成本或平均成本")]
        public decimal Cost { get; set; }

        [Option("share", HelpText = "份额")]
        public decimal Share { get; set; }

        [Option("amount", HelpText = "金额")]
        public decimal Amount { get; set; }

        [Option('o', "output", HelpText = "输出，默认控制台输出")]
        public string Output { get; set; } = "console";

        [Option("outputformat", HelpText = "输出格式，json description(文本说明)")]
        public string OutputFormat { get; set; } = "json";
    }
}
