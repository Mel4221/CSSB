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
        public void RunCommand(string function)
        {
            switch (function.ToUpper())
            {
                case "ADD":
                    if (this.BufferPackage.Name == "" || this.BufferPackage.Source == "")
                    {
                        throw new Exception($"MISSING PACKAGE NAME OR PACKAGE SOURCE URL");
                    }
                    //this.BinBuilder.Add(this.package);
                    Get.Pink($"ADDING PACKAGE: [{BufferPackage.Name}] TO: [{this.BinBuilder.FileName}]");
                    Get.Blue(BufferPackage.ToString());
                    this.BinBuilder.Add(BufferPackage);
                    BufferPackage = new Package();
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
    }
}
