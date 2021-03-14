using Butler.Common.Extensions;
using Butler.Models;
using CommandLine;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Butler.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var begin = DateTime.Now;
            System.Console.WriteLine(begin);
            await Parser.Default.ParseArguments<Options>(args)
                   .WithParsedAsync(async o =>
                   {
                       if (o.Target.ToLower() == "asset")
                       {
                           switch (o.Command)
                           {
                               case "Add":
                                   Asset.AddFundPosition(o.FundCode, o.Cost, o.Share);
                                   break;
                               case "Remove":
                                   Asset.RemoveFundPosition(o.FundCode);
                                   break;
                               case "Analyse":
                                   Output(o, await Asset.Analyse());
                                   break;
                               case "Clear":
                                   Asset.Clear();
                                   break;
                               default:
                                   System.Console.WriteLine("请参照 Readme.md 文件选择正确的指令代码");
                                   break;
                           }
                       }
                       else if (o.Target.ToLower() == "strategy")
                       {
                           switch (o.Command)
                           {
                               case "GetAlphaBasedOnIndustry":
                                   System.Console.WriteLine(await Strategy.GetAlphaBasedOnIndustry(o.FundCode));
                                   break;
                               case "GetFundTimingAbility":
                                   System.Console.WriteLine(@"首次计算基金择时能力需要一至两分钟的时间，请耐心等待...
计算过程中会使用到中证全指(000985.CSI)、中信金融(CI005917)、中信周期(CI005918)、中信消费(CI005919)、中信成长(CI005920)
但目前并未找到可以直接获取数据的数据源，因此需要用户自己从其他途径获取数据
目前程序仅支持 Choice 导出的数据，可参考 data 目录下的文件");
                                   System.Console.WriteLine(await Strategy.GetFundTimingAbility(o.FundCode));
                                   break;
                               default:
                                   System.Console.WriteLine("请参照 Readme.md 文件选择正确的指令代码");
                                   break;
                           }
                       }
                       else if (o.Target.ToLower() == "data")
                       {
                           switch (o.Command)
                           {
                               case "Init":
                                   Data.Init();
                                   break;
                               default:
                                   System.Console.WriteLine("请参照 Readme.md 文件选择正确的指令代码");
                                   break;
                           }
                       }
                       else if (o.Target.ToLower() == "market")
                       {
                           switch (o.Command)
                           {
                               case "Analyse":
                                   Output(o, Market.Analyse());
                                   break;
                               default:
                                   System.Console.WriteLine("请参照 Readme.md 文件选择正确的指令代码");
                                   break;
                           }
                       }
                       else if (o.Target.ToLower() == "ability")
                       {
                           switch (o.Command)
                           {
                               case "AnalyseFund":
                                   (await Ability.AnalyseFund(o.FundCode, "TimingAbility")).ForEach(x => Output(o, x));
                                   break;
                               default:
                                   System.Console.WriteLine("请参照 Readme.md 文件选择正确的指令代码");
                                   break;
                           }
                       }
                   });
            System.Console.WriteLine(DateTime.Now - begin);
        }

        private static void Output(Options o, object result)
        {
            switch (o.Output)
            {
                case "console":
                default:
                    System.Console.WriteLine(GetOutputString(o, result));
                    break;
            }
        }

        private static string GetOutputString(Options o, object result)
        {
            switch (o.OutputFormat)
            {
                case "description":
                    if (result is IDescription description)
                    {
                        return description.GetDescription();
                    }
                    else
                    {
                        return result.ToJson();
                    }
                case "json":
                default:
                    return result.ToJson();
            }
        }
    }
}
