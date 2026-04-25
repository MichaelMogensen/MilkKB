using DRDownloadWindow2.Types;

namespace DRDownloadWindow2.Utilities
{
    public class StatusAndProgressHandler
    {
        public UIDispatchUpdater<UIElementProps<string>> StatusBar { get; set; }
        public UIDispatchUpdater<UIElementProps<int>> ProgressBar { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="statusBar"></param>
        /// <param name="progressBar"></param>
        public StatusAndProgressHandler(
            UIDispatchUpdater<UIElementProps<string>> statusBar,
            UIDispatchUpdater<UIElementProps<int>> progressBar)
        {
            StatusBar = statusBar;
            ProgressBar = progressBar;
        }

        /// <summary>
        /// Update status.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="warningLevel"></param>
        /// <returns></returns>
        public void UpdateStatus(string message, EWarningLevel warningLevel = EWarningLevel.info)
        {
            StatusBar.Value = new UIElementProps<string>(message, warningLevel);
        }

        /// <summary>
        /// Update status and wait.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="waitInSec"></param>
        /// <param name="warningLevel"></param>
        /// <returns></returns>
        public async Task UpdateStatusAndWaitAsync(string message, int waitInSec, EWarningLevel warningLevel = EWarningLevel.info)
        {
            UpdateStatus(message, warningLevel);
            await Task.Delay(1000 * waitInSec);
        }

        /// <summary>
        /// Update progress.
        /// </summary>
        /// <param name="progress"></param>
        /// <param name="warningLevel"></param>
        public void UpdateProgress(int progress, EWarningLevel warningLevel = EWarningLevel.info)
        {
            ProgressBar.Value = new UIElementProps<int>(progress, warningLevel);
        }
    }
}

