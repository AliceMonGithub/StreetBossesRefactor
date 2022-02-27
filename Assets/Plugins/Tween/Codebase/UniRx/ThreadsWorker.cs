using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Tween.ThreadsWorker
{
    public static class ThreadsWorker
    {
        public static KeyValuePair<TType, CompositeDisposable> CreateThread<TType>(this Dictionary<TType, CompositeDisposable> threadsPool, TType type) where TType : class
        {
            threadsPool.ClearThread(type);

            threadsPool.Add(type, new CompositeDisposable());

            return threadsPool.FirstOrDefault(thread => thread.Key == type);
        }

        public static void ClearThread<TType>(this Dictionary<TType, CompositeDisposable> threads, TType key) where TType : class
        {
            if (threads.Count == 0) return;

            var objectThread = threads.FirstOrDefault(thread => thread.Key == key);

            if (objectThread.Key == null) return;

            objectThread.Value.Clear();

            threads.Remove(objectThread.Key);
        }
    }
}
