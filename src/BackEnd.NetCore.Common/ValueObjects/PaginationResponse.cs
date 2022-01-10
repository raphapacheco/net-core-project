using BackEnd.NetCore.Common.Repositories;
using System.Collections.Generic;

namespace BackEnd.NetCore.Common.ValueObjects
{
    public class PaginationResponse<TModelo> : ValueObject
        where TModelo : Model
    {
        public PaginationResponse(IReadOnlyList<TModelo> items, int page)
        {
            Items = items;
            Page = page;
        }

        public int Count => Items?.Count ?? 0;

        public IReadOnlyList<TModelo> Items { get; }

        public int Page { get; }

        public override object GetValue()
        {
            return $"{typeof(TModelo)}:{Page}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return typeof(TModelo);
            yield return Page;
        }
    }
}
