using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace CoreEscuela.Entidades
{
    /// <summary>
    /// abstract: no permite crear objeto de esta clase 
    /// </summary>
    public abstract class ObjetoEscuelaBase
    {
        public string UniqueId { get; private set; }
        public string Nombre { get; set; }
        public ObjetoEscuelaBase() => UniqueId = Guid.NewGuid().ToString();

        public override string ToString()
        {
            return $"{Nombre}, {UniqueId}";
        }
    }
}