using System;
using Tools.Documentador.Dtos;

namespace Tools.Documentador.Readers
{
    public interface IClassReader
    {
        IClassInfo ReadClass(Type type);
    }
}