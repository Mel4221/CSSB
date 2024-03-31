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
                    this.Add();
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
