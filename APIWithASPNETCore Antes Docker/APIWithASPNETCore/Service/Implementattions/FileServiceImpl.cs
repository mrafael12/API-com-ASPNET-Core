using APIWithASPNETCore.Data.VO;
using System.IO;

namespace APIWithASPNETCore.Service
{
    public class FileServiceImpl : IFileService
    {
        public byte[] GetPDFFile()
        {
            string path = Directory.GetCurrentDirectory();
            var fullPath = path + "\\Other\\Boleto-IPTV 02 2019 parte 2.pdf";
            return File.ReadAllBytes(fullPath);
        }
    }
}
