using System;
using System.IO;
using QuickTools.QCore;
using QuickTools.QData;
namespace ClownShellSourcesBuilder
{
    public partial class Builder
    {
        private bool Contains(char ch,string charactes)
        {
            for(int _ch =0; _ch < charactes.Length; _ch++)
            {
                if(charactes[_ch] == ch) return true; 
            }
            return false; 
        }

        private bool SafetyCheck(string file)
        {
            bool pass = true;
            String[] lines = File.ReadAllLines(file);
            for (int line = 0; line < lines.Length; line++)
            {
                if (!string.IsNullOrEmpty(lines[line]) && !string.IsNullOrWhiteSpace(lines[line]))
                {
                   // Get.Red(lines[line]);
                    if (!this.Contains(this.QKeyManager.KeyAssingChar,lines[line]) || !this.Contains(this.QKeyManager.KeyTerminatorChar, lines[line]))
                    {
                        this.FailLineNumber = line + 1; 
                        return false;
                    }
                    else
                    {
                        this.FileContent += $"{lines[line]}\n";
                    }
                }
            }
            return pass; 
        }
        public void Load()
        {
            string file;
            file = this.Args[0];
            if (!File.Exists(file)) throw new FileNotFoundException($"THE GIVEN FILE WAS NOT FOUND OR DOES NOT EXIST: [{file}]");
            if (!SafetyCheck(file)) throw new Exception($"SAFETY CHECK FAILED AT LINE: [{this.FailLineNumber}] MISSING EITHER ASSING CHAR OR TERMINATOR CHAR ASSING CHAR: [{this.QKeyManager.KeyAssingChar}] TERMINATOR CHAR: [{this.QKeyManager.KeyTerminatorChar}]");
            if (this.FileContent == null) throw new Exception("NO FILE CONTENT TO READ"); 

            QKeyManager.Keys = QKeyManager.ToKeyList(this.FileContent);
            Func<string, string> F = (input) => { return input == "" ? "VOID" : input; };
            QKeyManager.Keys.ForEach(item => Get.Yellow($"DETECTED: {item.Name} = {F(item.Value)}"));
        }
    }
}
