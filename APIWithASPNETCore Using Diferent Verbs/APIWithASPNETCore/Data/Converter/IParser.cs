﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIWithASPNETCore.Data.Converter
{
    public interface IParser<O, D>
    {
        D Parse(O origin);
        List<D> ParserList(List<O> origin);
    }
}
