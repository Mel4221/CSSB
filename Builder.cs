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
        private string[] args;
        private BinBuilder BinBuilder { get; set; } = new BinBuilder();
        public Builder()
        {
        }

        public Builder(string[] args)
        {
            this.args = args;
        }

       
      
      
        private string[] options { get; set; } = {"ADD PACKAGE","DELETE PACKAGE"};
        private bool CheckIntention()
        {
            Options option = new Options(this.options); 
            if(option.Pick() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Delete()
        {
            return;
        }

        public void Start()
        {
            if(this.args.Length > 0)
            {
                this.StartWithArgs();
                return;
            }if (!this.CheckIntention()) this.Delete();
             
            this.BinBuilder.AllowDeubbuger = true;
            this.BinBuilder.FileName = Get.Input("DROP OR TYPE THE SOURCES FILE PATH").Text;
            this.BinBuilder.Source = Get.Input("DROP OR TYPE THE BIN FOLDER PATH").Text;

            Package package = new Package();
            package.Name = Get.Input("PACKAGE NAME").Text;
            package.Source = Get.Input("PACKAGE SOURCE URL").Text;
            package.Creator = Get.Input("PACKAGE CREATOR").Text;
            package.Description = Get.Input("PACKAGE DESCRIPTION").Text;
            this.BinBuilder.Add(package);
            Get.Green($"DONE!!!");

        }
    }
}
