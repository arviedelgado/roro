using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Roro.Workspace.Framework
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected void OnPropertyChanged<T>(ref T prop, T value, [CallerMemberName] string name = default)
        {
            prop = value;
            OnPropertyChanged(name);
        }

        public void OnPropertyChanged(params string[] names)
        {
            names?.ToList().ForEach(name => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)));
        }
    }
}
