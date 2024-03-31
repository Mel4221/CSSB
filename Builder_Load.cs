using System;
using System.IO;
using QuickTools.QCore;
using QuickTools.QData;
namespace ClownShellSourcesBuilder
{
    public partial class Builder
    {
        public void Load()
        {
            string file, content;
            file = this.Args[0];
            if (!File.Exists(file)) throw new FileNotFoundException($"THE GIVEN FILE WAS NOT FOUND OR DOES NOT EXIST: [{file}]");
            content = File.ReadAllText(file);
            QKeyManager.Keys = QKeyManager.ToKeyList(content);
            Func<string, string> F = (input) => { return input == "" ? "VOID" : input; };
            QKeyManager.Keys.ForEach(item => Get.Yellow($"DETECTED: {item.Name} = {F(item.Value)}"));
        }
    }
}
