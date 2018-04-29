using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CTest
{
    public delegate T MDelegate<T>();
    
    public class MyAsyncResult<T> : IAsyncResult
    {
        private volatile bool _isCompleted;
        private readonly ManualResetEvent _asyncWaitHandle;
        private readonly AsyncCallback _callback;
        private readonly object _asyncState;

        private Exception _exception;
        private T _result;

        public MyAsyncResult(MDelegate<T> work, AsyncCallback callback, object state)
        {
            _callback = callback;
            _asyncState = state;
            _asyncWaitHandle = new ManualResetEvent(false);

            RunWorkAsynchronously(work);
        }

        public object AsyncState
        {
            get
            {
                return _asyncState;
            }
        }

        public WaitHandle AsyncWaitHandle
        {
            get
            {
                return _asyncWaitHandle;
            }
        }

        public bool IsCompleted
        {
            get
            {
                return _isCompleted;
            }
        }

        public bool CompletedSynchronously
        {
            get
            {
                return false;
            }
        }

        private void RunWorkAsynchronously(MDelegate<T> work)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    _result = work();
                }
                catch (Exception e)
                {
                    _exception = e;
                }
                finally
                {
                    _isCompleted = true;
                    _asyncWaitHandle.Set();
                    _callback?.Invoke(this);
                }
            });
        }

        public T End()
        {
            if (!_isCompleted)
            {
                _asyncWaitHandle.WaitOne();
                _asyncWaitHandle.Close();
            }
            if (_exception != null)
            {
                throw _exception;
            }

            return _result;
        }
    }
}