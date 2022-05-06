﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Repository.Oracle.classes
{
    public enum TipoDaCodificaoDoArquivo
    {
        Default = 0,
        ASCII = 1,
        UTF7 = 2,
        UTF8 = 3,
        UTF32 = 4,
        Unicode = 5,
        BigEndianUnicode = 6,
    }
}
