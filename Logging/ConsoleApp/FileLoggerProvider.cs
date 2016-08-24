using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;

namespace LoggingSample
{
    // This is not a robust file logger, and only exists the purpose of showing
    // how a custom logger can filter on event IDs. 
    // Filters on event ID may be added into logging abstractions at some point (cref https://github.com/aspnet/Logging/issues/233)
    // but for now the only way to filter is to implement your own provider
    // TODO use a "real" file logger. See https://github.com/aspnet/Logging/issues/441
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly StreamWriter _writer;
        private readonly EventFilterDelegate _filter;

        public delegate bool EventFilterDelegate(string categoryName, EventId id);

        public FileLoggerProvider(string filename, EventFilterDelegate filter = null)
        {
            var stream = new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            _writer = new StreamWriter(stream, Encoding.UTF8, 1024 * 10, leaveOpen: false)
            {
                AutoFlush = true
            };
            _filter = filter;
        }

        public ILogger CreateLogger(string categoryName)
            => new FileLogger(categoryName, this);

        private FileLoggerProvider TryWrite(string message)
        {
            if (!_disposed)
            {
                _writer.Write(message);
            }
            return this;
        }

        private FileLoggerProvider TryWriteLine(string message)
        {
            if (!_disposed)
            {
                _writer.WriteLine(message);
            }
            return this;
        }

        private volatile bool _disposed;

        public void Dispose()
        {
            _disposed = true;
            _writer.Dispose();
        }

        private class NullScope : IDisposable
        {
            public static IDisposable Instance = new NullScope();
            public void Dispose()
            {
            }
        }

        private class FileLogger : ILogger
        {
            private readonly string _name;
            private readonly FileLoggerProvider _parent;
            public FileLogger(string name, FileLoggerProvider parent)
            {
                _name = name;
                _parent = parent;
            }
            public IDisposable BeginScope<TState>(TState state)
                => NullScope.Instance;

            public bool IsEnabled(LogLevel logLevel)
                => !_parent._disposed;

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                if (_parent._filter?.Invoke(_name, eventId) != false)
                {
                    _parent.TryWrite(_name)
                        .TryWrite(": ")
                        .TryWriteLine(formatter(state, exception));
                }
            }
        }
    }
}