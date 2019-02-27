using APIWithASPNETCore.Data.VO;
using APIWithASPNETCore.Model;

namespace APIWithASPNETCore.Service
{
    public interface IFileService
    {        
        byte[] GetPDFFile();        
    }
}
