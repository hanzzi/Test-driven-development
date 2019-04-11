namespace InvoiceService
{
    interface IFileSystem
    {
        File GetFile(int authLevel, int fileId);
    }
}