using System;
using QuickTools.QData;
using System.Collections.Generic;
using System.Text;
using QuickTools.QCore;
using System.IO;
using QuickTools.QIO;
using QuickTools.QConsole;
using System.Diagnostics;
using QuickTools.QDevelop;


namespace ClownShellSourcesBuilder
{
    public partial class Builder
    {
        private void CallFunction(ref Key function)
        {
            string f, parameter, fSignature;
            int number;
            f = function.Name;
            f = f.Substring(0, f.IndexOf('('));
            fSignature = function.Name;

            parameter = fSignature.Substring(fSignature.IndexOf('('));
            parameter = parameter.Replace("(", "");
            parameter = parameter.Replace(")", "");
            if (this.AllowDebugger) Get.Yellow($"DIRECT CALL");
            if (this.AllowDebugger) Get.Yellow($"FUNCTION DETECTED: [{f}] PARAMETER: [{parameter}]");
            if (parameter.Length > 0)
            {
                if (parameter[0] == '$')
                {
                    Variable v = this.Stack.GetVariable(parameter.Replace("$", ""));
                    parameter = v.Value;
                    if (this.AllowDebugger) Get.Red(v.ToString());
                }

            }
            switch (f)
            {
                case "DELETE":
                    IsDeleting = true; 
                    this.Delete(parameter);
                    IsDeleting = false; 
                    break;
                case "DELETE-ALL":

                        if (this.AllowDebugger) Get.Red($"DELETING ALL PACKAGES FROM: {this.BinBuilder.Source}");
                        ///this.BinBuilder.Packages.Clear();
                        this.BinBuilder.Clear(); 
                    break;
                case "ADD":
                    this.Add();
                    break;
                case "SYS_CALL":
                    if (this.AllowDebugger) Get.Yellow($"SYSTEM CALL: [{parameter}] ARGS: [{function.Value}]");
                    ProcessStartInfo info = new ProcessStartInfo();
                    info.FileName = parameter;
                    info.Arguments = function.Value;
                    Process exe = Process.Start(info);
                    //exe.WaitForExit();
                    break;
                case "PRINT-ALL":
                    BinBuilder bin = new BinBuilder();
                    bin.FileName = this.BinBuilder.FileName;
                    bin.AllowDeubbuger = this.BinBuilder.AllowDeubbuger;
                    bin.Load();
                    bin.Packages.ForEach(p => Get.Green(p.ToString()));
                    Get.Yellow($"PACKAGES COUNT: [{bin.Packages.Count}]");
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
                case "STOP":
                    Get.Wait();
                    break;
                case "EXIT":
                    if (this.AllowDebugger) Get.Green("bye");
                    Environment.Exit(0);
                    return;
                default:
                    throw new NotImplementedException($"DIRECT CALL NOT IMPLEMENTED FOR THIS FUNCTION: [{function}]");
            }
        }
    }
}
