using System.Collections.Generic;
using APIWithASPNETCore.Model;
using APIWithASPNETCore.Repository.Generic;
using APIWithASPNETCore.Data.Converters;
using APIWithASPNETCore.Data.VO;

namespace APIWithASPNETCore.Service
{
    public class BookServiceImpl : IBookService
    {
        private IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookServiceImpl(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }        

        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Create(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<BookVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }        

        public BookVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Update(bookEntity);
            return _converter.Parse(bookEntity);
        }        
    }
}
