using MiniTool;
namespace ClownShellSourcesBuilder
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            QuickToolsDownloader downloader = new QuickToolsDownloader();
            downloader.Dowload(); 
            Builder builder; 
             if(args.Length > 0)
            {
                builder = new Builder(args);
                builder.Start();
                return;
            }
            else
            {
                return;
            }

        }
    }
}
