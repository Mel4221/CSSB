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
        public void Start()
        {
            if (this.Args.Length > 0)
            {
                this.StartWithArgs();
                return;
            }
            Get.Red(new NotImplementedException().Message);
            /*
            if (!this.CheckIntention()) this.Delete();

            this.BinBuilder.AllowDeubbuger = true;
            this.BinBuilder.FileName = Get.Input("DROP OR TYPE THE SOURCES FILE PATH").Text;
            this.BinBuilder.Source = Get.Input("DROP OR TYPE THE BIN FOLDER PATH").Text;

            package.Name = Get.Input("PACKAGE NAME").Text;
            package.Source = Get.Input("PACKAGE SOURCE URL").Text;
            package.Creator = Get.Input("PACKAGE CREATOR").Text;
            package.Description = Get.Input("PACKAGE DESCRIPTION").Text;
            this.BinBuilder.Add(package);
            Get.Green($"DONE!!!");
            */
        }
    }
}
