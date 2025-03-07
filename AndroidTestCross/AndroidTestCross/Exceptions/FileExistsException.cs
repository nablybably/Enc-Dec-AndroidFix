using System;

namespace CrossPlatformTest.Exceptions;

public class FileExistsException : Exception
{
    public FileExistsException(string fileName) : base(fileName)
    {
        
    }
}