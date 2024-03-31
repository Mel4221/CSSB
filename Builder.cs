using System;
using QuickTools.QData;
using System.Collections.Generic;
using System.Text;
using QuickTools.QCore;
using System.IO;
using QuickTools.QIO;
using QuickTools.QConsole;
using QuickTools.QDevelop;


namespace ClownShellSourcesBuilder
{
    public partial class Builder
    {
        private string[] Args { get; set; } =  new string[] { };
        private BinBuilder BinBuilder { get; set; } = new BinBuilder();


        public Builder(string[] args)
        {
            this.Args = args;
        }
        public void Start()
        {
            this.StartWithArgs(); 
        }

    }
}
