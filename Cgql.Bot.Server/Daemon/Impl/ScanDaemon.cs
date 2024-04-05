using Cgql.Bot.Model.Database;

namespace Cgql.Bot.Server.Daemon.Impl;

/// <summary>
///     Background daemon service for scan.
///     FIXME: The multi-thread is poorly designed here. :(
/// </summary>
public class ScanDaemon : IScanDaemon
{
    private const int MaxThreads = 5;
    private const int MaxSemaphore = 128;

    private readonly ILogger<ScanDaemon> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IApiDaemon _api;

    private readonly object _mutex = new();
    private readonly Semaphore _semaphore = new(0, MaxSemaphore);

    private readonly Queue<ScanTask> _queue = new();
    private readonly List<Tuple<Thread, CancellationTokenSource>> _threads = [];

    public ScanDaemon(ILogger<ScanDaemon> logger, IServiceScopeFactory serviceScopeFactory, IApiDaemon api)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _api = api;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Establishing battle field control, stand by.");

        for (int i = 0; i < MaxThreads; i++)
        {
            CancellationTokenSource tokenSource = new();
            Thread thread = new(() => ScanThread(tokenSource.Token));
            _threads.Add(new Tuple<Thread, CancellationTokenSource>(thread, tokenSource));
            thread.Start();
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping scan task...");

        _threads.ForEach(tuple => tuple.Item2.Cancel());
        _semaphore.Release(MaxThreads);
        _threads.ForEach(tuple => tuple.Item1.Join());
        _threads.ForEach(tuple => tuple.Item2.Dispose());
        _semaphore.Dispose();

        _logger.LogInformation("Battle control terminated.");

        return Task.CompletedTask;
    }


    public void AddTask(ScanTask task)
    {
        lock (_mutex)
        {
            _queue.Enqueue(task);
        }

        _semaphore.Release(1);
    }

    public void DropAllTasks()
    {
        lock (_mutex)
        {
            while (_queue.Count > 0)
            {
                _queue.Dequeue();
                _semaphore.WaitOne();
            }
        }
    }

    public void Dispose()
    {
    }

    private void ScanThread(CancellationToken token)
    {
        ScanTask task;
        while (!token.IsCancellationRequested)
        {
            _semaphore.WaitOne();
            lock (_mutex)
            {
                if (_queue.Count > 0)
                {
                    task = _queue.Dequeue();
                }
                else
                {
                    continue;
                }
            }

            if (token.IsCancellationRequested)
            {
                break;
            }

            _logger.LogInformation("Task {taskId} started", task.Id);
            // TODO: Perform scan

            Thread.Sleep(5000);
            // TODO: Get result

            // TODO: Send message
            _logger.LogInformation("Task {taskId} handled", task.Id);
            _api.SendResult(task);
        }
    }
}