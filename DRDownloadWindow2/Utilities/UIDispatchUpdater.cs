using System.Windows;

namespace DRDownloadWindow2.Utilities
{
    /// <summary>
    /// Set value in a UI element.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UIDispatchUpdater<T>
    {
        private Action<T> Update { get; set; }

        public T Value { set => Update(value); }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="update"></param>
        public UIDispatchUpdater(Action<T> update)
        {
            Update = value => Application.Current.Dispatcher.Invoke(() => update(value));
        }

    }
}

