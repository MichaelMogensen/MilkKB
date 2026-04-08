namespace DRDownload.Pipe
{
    public class PipeOutputWindow : PipeOutputBase // TODO: Move to WPF program.
    {
        public PipeOutputWindow()
        {
            
        }

        public override void PipeMessageTo(string msg = "")
        {
            // TODO: To list view.
        }

        public override void PipeProgressTo(string progress)
        {
            // NOP.
        }

        public override void PipeProgressTo(int progress)
        {
            // TODO: To progress bar.
        }
    }
}

