
namespace ClownShellSourcesBuilder
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Builder builder; 
             if(args.Length > 0)
            {
                builder = new Builder(args);
                builder.Start();
                return;
            }
            else
            {
                builder = new Builder();
                builder.Start(); 
            }

        }
    }
}
