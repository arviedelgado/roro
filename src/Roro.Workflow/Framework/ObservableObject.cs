using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Roro.Workflow.Framework
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged<T>(ref T property, T propertyValue, [CallerMemberName] string propertyName = null)
        {
            property = propertyValue;
            NotifyPropertyChanged(propertyName);
        }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null, params string[] propertyNames)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            propertyNames.ToList().ForEach(x => NotifyPropertyChanged(x));
        }
    }
}
