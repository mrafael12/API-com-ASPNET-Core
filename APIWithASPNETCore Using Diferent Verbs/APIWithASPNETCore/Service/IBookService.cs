using APIWithASPNETCore.Data.VO;
using System.Collections.Generic;

namespace APIWithASPNETCore.Service
{
    public interface IBookService
    {
        BookVO Create(BookVO book);
        BookVO FindById(long id);
        List<BookVO> FindAll();
        BookVO Update(BookVO book);
        void Delete(long id);
    }
}
