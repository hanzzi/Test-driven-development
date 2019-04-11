using System;

namespace InvoiceService
{
    public class FileSupplier : IFileSystem
    {
        private File[] _files;

        private bool AuthenticatePriviledge(int Authlevel, int fileId)
        {
            throw new NotImplementedException();
        }

        public File GetFile(int authlevel, int fileId) {
            throw new NotImplementedException();
        }


    }
}
