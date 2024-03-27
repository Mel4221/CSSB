using System;
using QuickTools.QData;
using System.Collections.Generic;
using System.Text;
using QuickTools.QCore;
using System.IO;
using QuickTools.QIO;
using QuickTools.QConsole;
namespace ClownShellSourcesBuilder
{
    public partial class Builder
    {
        private QKeyManager manager = new QKeyManager();
        private Package package = new Package();

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
                    Get.Green($"ADDING PACKAGE: [{package.Name}] TO: [{this.BinBuilder.FileName}]");
                    Get.Blue(package.ToString());
                    package = new Package();
                    break;
                case "DELETE":
                    Get.Yellow($"DELETING FLAG ACTIVE");
                    this.IsDeleting = true; 
                    break;
                case "SLEEP":
                    Get.WaitTime(1000);
                    break;
                default:
                    throw new Exception("INVALID FUNCTION DETECTED");
            }
        }
        private bool IsDeleting { get; set; } = false; 
        public void StartWithArgs()
        {
            string file, content;
            this.manager.AllowDebugger = true;
            this.BinBuilder.AllowDeubbuger = true;
            file = args[0];
            if (!File.Exists(file)) throw new FileNotFoundException($"THE GIVEN FILE WAS NOT FOUND OR DOES NOT EXIST: [{file}]");
            content = File.ReadAllText(file)    ;
            manager.Keys = QKeyManager.ToKeyList(content);
            manager.Keys.ForEach(item => Get.Yellow($"DETECTED: [{item.ToString()}]"));
            Key key;
            for (int item = 0; item < manager.Keys.Count; item++)
            {
                key = manager.Keys[item];
                switch (key.Name.ToUpper())
                {

                    case "FUNCTION":
                        this.RunCommand(key.Value);
                        break;
                    case "BIN_PATH":
                        Get.Green($"BIN PATH DETECTED: [{key.Value}]");
                        this.BinBuilder.Source = key.Value;
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
                        throw new Exception("INVALID FUNCTION DETECTED");
                }
                //Get.WaitTime(500);
            }

        }
    }
}
