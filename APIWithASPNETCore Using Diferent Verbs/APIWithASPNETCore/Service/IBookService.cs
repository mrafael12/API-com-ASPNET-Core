﻿using APIWithASPNETCore.Model;
using System.Collections.Generic;

namespace APIWithASPNETCore.Service
{
    public interface IBookService
    {
        Book Create(Book book);
        Book FindById(long id);
        List<Book> FindAll();
        Book Update(Book book);
        void Delete(long id);
    }
}
