namespace LinkShortener.Service;

public class Counter
{
    private ulong _count;

    public ulong GetNext()
    {
        var current = GetCount();
        Interlocked.Increment(ref _count);
        return current;
    }

    public ulong GetCount()
    {
        return Interlocked.Read(ref _count);
    }
}
