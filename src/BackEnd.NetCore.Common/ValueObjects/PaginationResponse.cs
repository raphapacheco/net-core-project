using BackEnd.NetCore.Common.Models;
using System;
using System.Collections.Generic;

namespace BackEnd.NetCore.Common.ValueObjects
{
    public class PaginationResponse<TModelo> : ValueObject
        where TModelo : Model
    {
        public PaginationResponse(IReadOnlyList<TModelo> items, int totalCount, int page, int size)
        {
            Items = items;
            Page = page;
            Size = size;
            TotalCount = totalCount;
        }

        public int Count => Items?.Count ?? 0;
        public int TotalPages => Convert.ToInt32(Math.Truncate((decimal) TotalCount / Size) + (TotalCount % Size > 0 ? 1 : 0));

        public IReadOnlyList<TModelo> Items { get; }

        public int Size { get; }
        public int Page { get; }
        public int TotalCount { get; }

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
