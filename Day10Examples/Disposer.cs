// Best Practice is to use Dispose(). This is better than relying only on a Finalizer
class MyResource : IDisposable
{
    public void Dispose()
    {
         // Cleanup Resources
        GC.SuppressFinalize(this);
    }

    ~MyResource()
    {
        // Backup cleanup
        Dispose();
    }
}