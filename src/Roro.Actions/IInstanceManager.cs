using System;

namespace Roro.Actions
{
    public interface IInstanceManager
    {
        public T Get<T>(Guid id);

        public Guid Create<T>(T obj) where T : IDisposable => Create(obj, x => x.Dispose());

        public Guid Create<T>(T obj, Action<T> dispose);

        public void Dispose(Guid id);
    }
}
