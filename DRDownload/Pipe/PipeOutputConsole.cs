using DRDownload.Common;

namespace DRDownload.Pipe
{
    public class PipeOutputConsole : PipeOutputBase
    {
        public PipeOutputConsole() : base()
        {

        }

        public override void PipeMessageTo(string msg = "")
        {
            Console.WriteLine(msg);
        }

        public override void PipeProgressTo(string progress)
        {
            Util.WriteInFixedConsolePosition(progress);
        }

        public override void PipeProgressTo(int progress)
        {
            // NOP.
        }
    }
}

