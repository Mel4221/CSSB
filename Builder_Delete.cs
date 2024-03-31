using System;
using QuickTools.QCore;

namespace ClownShellSourcesBuilder
{
    public partial class Builder
    {
        private void Delete(string packageName)
        {
            if (this.IsDeleting)
            {
                if (this.AllowDebugger) Get.Red($"DELETING PACKAGE: [{packageName}] FROM: [{this.BinBuilder.FileName}]");
                this.BinBuilder.Delete(packageName);
                this.IsDeleting = false;
            }
        }
    }
}
