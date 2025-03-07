using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace AndroidTestCross.Classes;

public class FileArchiver
{
    //Makes a .zip archive from a file to give it a password, filename inside the archive will be xxx.xxxx to prevent peeping.
    //Variables: filePath, fileName, [Optional] passWord, [Optional] zippedFileName(empty for default), archivePath
    // @ param: filePath: 
    // @ param: fileName: 
    // @ param: [optional] zippedFileName: 
    // @ param: [optional] archivePath: 
    /// <summary>
    /// Archive a file
    /// </summary>
    /// <param name="filePath">path to file(basename)</param>
    /// <param name="fileName">name of file</param>
    /// <param name="zippedFileName">name of output file</param>
    /// <param name="archivePath">path of output file</param> 
    public void testArchive(string filePath, string fileName, string zippedFileName = "", string archivePath = "")
    {
        archivePath = archivePath == string.Empty ? filePath : archivePath;
        zippedFileName = zippedFileName == string.Empty ? fileName : zippedFileName;
        
        using (ZipOutputStream zipStream = new ZipOutputStream(File.Create("/home/nably/Test.zip")))
        {
            zipStream.Password = "Goeiendag";
            
            ZipEntry entry = new ZipEntry("xxxx.xxxx");
            zipStream.PutNextEntry(entry);
            
            byte[] buffer = File.ReadAllBytes("/home/nably/test.xlsx");
            
            zipStream.Write(buffer, 0, buffer.Length);
            
            zipStream.CloseEntry();
        }   
    }
    
    //Decompress an archive to get the stored file, variables: filePath, fileName, [Optional] passWord, [Optional] unzippedFileNamen, [Optional] unzippedFilePath
    //If possible also an option to, instead of decompressing and then reading the file, reading the file directly without decompressing.
    //Else use temporary files.
}