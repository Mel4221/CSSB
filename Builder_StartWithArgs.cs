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
    
 

       

        public void StartWithArgs()
        {
           

            //new Thread(() => { Console.Title += this.BinBuilder.CurrentStatus; 
            //.WaitTime(100);
            //   }).Start();
            this.Load(); 
            Key key;

            for (int item = 0; item < QKeyManager.Keys.Count; item++)
            {
                key = QKeyManager.Keys[item];
                switch (key.Name)
                {
                    case "DEBUGGER":
                        this.AllowDebugger = key.Value == "ON";
                        this.QKeyManager.AllowDebugger = this.AllowDebugger;
                        this.BinBuilder.AllowDeubbuger = this.AllowDebugger;
                        break;
                    case "VAR":
                        this.Stack.SetVariable(key.Value, "NULL");
                        break;
                    case "FUNCTION":
                        this.RunCommand(key.Value);
                        break;
                    case "BIN_PATH":
                        Get.Green($"BIN PATH DETECTED: [{key.Value}]");
                        this.BinBuilder.Source = key.Value;
                        break;
                    case "BRANCH":
                        this.BufferPackage.Branch = key.Value;
                        break;
                    case "NAME":
                        if (!this.IsDeleting)
                        {
                            Get.Green($"PACKAGE NAME DETECTED: [{key.Value}]");
                            BufferPackage.Name = key.Value;
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
                        BufferPackage.Source = key.Value;
                        break;
                    case "CREATOR":
                        Get.Green($"PACKAGE CREATOR DETECTED: [{key.Value}]");
                        BufferPackage.Creator = key.Value;
                        break;
                    case "DESCRIPTION":
                        Get.Green($"PACKAGE DESCRIPTION DETECTED: [{key.Value}]");
                        BufferPackage.Description = key.Value;
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

                            this.Stack.UpdateVariable(v, key.Value);
                            Get.Print(v, key.Value);
                            break;
                        }
                        if (key.Name.Contains("(") &&
                        key.Name.Contains(")"))
                        {
                            this.CallFunction(ref key);
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
