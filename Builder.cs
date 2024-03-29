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
        private string[] Args { get; set; } =  new string[] { };
        private BinBuilder BinBuilder { get; set; } = new BinBuilder();

        public Builder()
        {
        }

        public Builder(string[] args)
        {
            this.Args = args;
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
            this.BinBuilder.AllowDeubbuger = true;
            this.BinBuilder.FileName = Get.Input("DROP OR TYPE THE SOURCES FILE PATH").Text;
            this.BinBuilder.Delete(Get.Input("PACKAGE NAME").Text);
            return;
        }

 
    }
}
