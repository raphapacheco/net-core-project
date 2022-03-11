using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace BackEnd.NetCore.Common.DataContracts
{
    [ExcludeFromCodeCoverage]
    public abstract class ConsultarPaginadoResponseBase<TResponse>
    {
        public int Pagina { get; set; }
        
        public int Tamanho { get; set; }
        
        public int TotalPaginas { get; set; }

        public int Quantidade => Itens?.ToList().Count ?? 0;        

        public IEnumerable<TResponse> Itens { get; set; }
    }
}
