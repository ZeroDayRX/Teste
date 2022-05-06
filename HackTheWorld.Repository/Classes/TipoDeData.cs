using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Repository.Classes
{
    public enum TipoDeData
    {
        DataHora_DDMMYYYYHHMMSS,
        DataHora_DDMMYYYYHHMM,
        DataHora_DDMMYYHHMMSS,
        DataHora_DDMMYYHHMM,
        DataHora_YYYYMMDDHHMMSS,
        DataHora_YYYYMMDDHHMM,
        DataHora_YYYYMMDD,
        Data_DDMMYYYY,
        Data_DDMMYY,
        Data_YYMM,
        Data_YYYYMM,
        Data_YYYY,
        Hora_HHMMSS,
        Hora_HHMM
    }
}
