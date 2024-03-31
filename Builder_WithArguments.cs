using System;
using QuickTools.QData;
using System.Collections.Generic;
using System.Text;
using QuickTools.QCore;
using System.IO;
using QuickTools.QIO;
using QuickTools.QConsole;
using System.Threading;
using System.Diagnostics;

namespace ClownShellSourcesBuilder
{
    public partial class Builder
    {
        private QKeyManager manager = new QKeyManager();
        private Package package = new Package();
        private VirtualStack stack = new VirtualStack(); 

        public void RunCommand(string function)
        {
            switch (function.ToUpper())
            {
                case "ADD":
                    if(this.package.Name == "" || this.package.Source == "")
                    {
                        throw new Exception($"MISSING PACKAGE NAME OR PACKAGE SOURCE URL");
                    }
                    //this.BinBuilder.Add(this.package);
                    Get.Pink($"ADDING PACKAGE: [{package.Name}] TO: [{this.BinBuilder.FileName}]");
                    Get.Blue(package.ToString());
                    this.BinBuilder.Add(package); 
                    package = new Package();
                    break;
                case "DELETE":
                    Get.Yellow($"DELETING FLAG ACTIVE");
                    this.IsDeleting = true; 
                    break;
                case "DELET-ALL":
                    File.Delete(this.BinBuilder.FileName);
                    break; 
                default:
                    throw new Exception("INVALID FUNCTION DETECTED");
            }
        }

        private void ProcessCallFunction(ref Key function)
        {
            string f, parameter;
            int number;
            f = function.Name;
            f = f.Substring(0, f.IndexOf('('));
            parameter = f.Substring(f.IndexOf('('));
            parameter = parameter.Replace("(","");
            parameter = parameter.Replace(")","");
            //Get.Yellow($"DIRECT CALL");
            //Get.Yellow($"FUNCTION DETECTED: [{f}] PARAMETER: [{parameter}]");
            if(parameter.Length > 0)
            {
                if (parameter[0] == '$')
                {
                    Variable v = this.stack.GetVariable(parameter.Replace("$", ""));
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
        private bool IsDeleting { get; set; } = false; 

        public void StartWithArgs()
        {
            string file, content;
            this.manager.AllowDebugger = true;
            this.BinBuilder.AllowDeubbuger = true;
            //new Thread(() => { Console.Title += this.BinBuilder.CurrentStatus; 
            //.WaitTime(100);
              //   }).Start();
            file = this.Args[0];
            if (!File.Exists(file)) throw new FileNotFoundException($"THE GIVEN FILE WAS NOT FOUND OR DOES NOT EXIST: [{file}]");
            content = File.ReadAllText(file)    ;
            manager.Keys = QKeyManager.ToKeyList(content);
            Func<string,string> F = (input) => { return input == "" ? "VOID" : input; };
            manager.Keys.ForEach(item => Get.Yellow($"DETECTED: {item.Name} = {F(item.Value)}"));
            Key key;
            for (int item = 0; item < manager.Keys.Count; item++)
            {
                key = manager.Keys[item];
                switch (key.Name)
                {

                    case "VAR":
                        stack.SetVariable(key.Value, "NULL");
                        break;
                    case "FUNCTION":
                        this.RunCommand(key.Value);
                        break;
                    case "BIN_PATH":
                        Get.Green($"BIN PATH DETECTED: [{key.Value}]");
                        this.BinBuilder.Source = key.Value;
                        break;
                    case "BRANCH":
                        this.package.Branch = key.Value;
                        break;
                    case "NAME":
                        if (!this.IsDeleting)
                        {
                            Get.Green($"PACKAGE NAME DETECTED: [{key.Value}]");
                            package.Name = key.Value;
                        }
                        if (this.IsDeleting)
                        {
                            Get.Red($"DELETING PACKAGE: [{key.Value}] FROM: [{this.BinBuilder.FileName}]");
                            this.BinBuilder.Delete(key.Value);
                            this.IsDeleting = false;
                        }
                        break;
                    case "SOURCE_URL":
                        Get.Green($"PACKAGE SOURCE URL DETECTED: [{key.Value}]");
                        package.Source = key.Value;
                        break;
                    case "CREATOR":
                        Get.Green($"PACKAGE CREATOR DETECTED: [{key.Value}]");
                        package.Creator = key.Value;
                        break;
                    case "DESCRIPTION":
                        Get.Green($"PACKAGE DESCRIPTION DETECTED: [{key.Value}]");
                        package.Description = key.Value;
                        break;
                    case "SOURCES_FILE":
                        Get.Green($"PACKAGE SOURCES FILE DETECTED: [{key.Value}]");
                        this.BinBuilder.FileName = key.Value;
                        break;
                    default:
                        //$VARIABLE=/P/D/D/F;

                        if (key.Name[0] == '$')
                        {
                            string v = key.Name.Replace("$","");

                            this.stack.UpdateVariable(v, key.Value);
                            Get.Print(v, key.Value);
                            break;
                        }
                        if (key.Name.Contains("(") &&
                        key.Name.Contains(")"))
                        {
                            this.ProcessCallFunction(ref key);
                            break;
                        }
                        else
                        {
                            throw new Exception($"INVALID FUNCTION DETECTED: [{key.Name}]");
                        }
                }
                //Get.WaitTime(500);
            }

        }
    }
}
