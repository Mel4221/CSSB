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
using QuickTools.QDevelop;

namespace ClownShellSourcesBuilder
{
    public partial class Builder
    {
        private void Add()
        {
            if (this.BufferPackage.Name == "" || this.BufferPackage.Source == "")
            {
                throw new Exception($"MISSING PACKAGE NAME OR PACKAGE SOURCE URL");
            }
            //this.BinBuilder.Add(this.package);
            if (this.AllowDebugger) Get.Pink($"ADDING PACKAGE: [{BufferPackage.Name}] TO: [{this.BinBuilder.FileName}]");
            if (this.AllowDebugger) Get.Blue(BufferPackage.ToString());
            this.BinBuilder.Add(BufferPackage);
            BufferPackage = new Package();
        }
    }
}
