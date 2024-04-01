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
        private QKeyManager QKeyManager = new QKeyManager();
        private Package BufferPackage = new Package();
        private VirtualStack Stack = new VirtualStack();
        private bool AllowDebugger { get; set; } = true;
        private bool IsDeleting { get; set; } = false;
        private string FileContent { get; set; } = null;
        private int FailLineNumber { get; set; } = 0; 

    }
}
