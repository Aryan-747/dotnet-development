using System;
class FileHandler
{
    private IntPtr unmanagedResource;

    public FileHandler()
    {
        // Allocate unmanaged resource
        unmanagedResource = new IntPtr(123);
    }

    ~FileHandler()
    {
        // Free unmanaged resource
        unmanagedResource = IntPtr.Zero;
        Console.WriteLine("Finalizer Called.");
    }
}