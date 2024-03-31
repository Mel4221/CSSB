using System;
using QuickTools.QData;
using System.Collections.Generic;
using System.Text;
using QuickTools.QCore;
using System.IO;
using QuickTools.QIO;
using QuickTools.QConsole;
using System.Diagnostics;

namespace ClownShellSourcesBuilder
{
    public partial class Builder
    {
        private void ProcessCallFunction(ref Key function)
        {
            string f, parameter;
            int number;
            f = function.Name;
            f = f.Substring(0, f.IndexOf('('));
            parameter = f.Substring(f.IndexOf('('));
            parameter = parameter.Replace("(", "");
            parameter = parameter.Replace(")", "");
            //Get.Yellow($"DIRECT CALL");
            //Get.Yellow($"FUNCTION DETECTED: [{f}] PARAMETER: [{parameter}]");
            if (parameter.Length > 0)
            {
                if (parameter[0] == '$')
                {
                    Variable v = this.Stack.GetVariable(parameter.Replace("$", ""));
                    parameter = v.Value;
                    //Get.Red(v.ToString());
                }

            }
            switch (f)
            {
                case "SYS_CALL":
                    ProcessStartInfo info = new ProcessStartInfo();
                    info.FileName = parameter;
                    info.Arguments = function.Value;
                    Process exe = Process.Start(info);
                    exe.WaitForExit();
                    break;
                case "PRINT-ALL":
                    BinBuilder bin = new BinBuilder();
                    bin.FileName = this.BinBuilder.FileName;
                    bin.AllowDeubbuger = this.BinBuilder.AllowDeubbuger;
                    bin.Load();
                    bin.Packages.ForEach(p => Get.Green(p.ToString()));
                    Get.Yellow($"Packages in sources file: [{bin.Packages.Count}]");
                    return;
                case "ECHO":
                case "PRINT":
                    Get.Box(parameter);
                    return;
                case "SLEEP":
                    if (Get.IsNumber(parameter))
                    {
                        number = int.Parse(parameter);
                        Get.WaitTime(number);
                        return;
                    }
                    else
                    {
                        throw new ArgumentException($"INVALID ARGUMENT GIVEN , EXPECTED A NUMBER IN THE GIVEN PARAMETER: [{parameter}]");
                    }
                case "EXIT":
                    Get.Green("bye");
                    Environment.Exit(0);
                    return;
                default:
                    throw new NotImplementedException($"DIRECT CALL NOT IMPLEMENTED FOR THIS FUNCTION: [{function}]");
            }
        }
    }
}
