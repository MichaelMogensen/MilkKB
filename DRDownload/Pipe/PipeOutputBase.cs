namespace DRDownload.Pipe
{
    public abstract class PipeOutputBase
    {
        protected PipeOutputBase()
        {
            
        }

        public abstract void PipeMessageTo(string msg = "");
        public abstract void PipeProgressTo(string progress);
        public abstract void PipeProgressTo(int progress);
    }
}

