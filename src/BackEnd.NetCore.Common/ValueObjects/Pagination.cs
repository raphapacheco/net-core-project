using System.Collections.Generic;

namespace BackEnd.NetCore.Common.ValueObjects
{
    public class Pagination : ValueObject
    {
        public Pagination(int page, int size)
        {
            Page = page;
            Size = size;
        }

        public int Page { get; }

        public int Size { get; }

        public int Skip => (Page - 1) * Size;

        public override object GetValue()
        {
            return $"{Page}:{Size}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Page;
            yield return Size;
        }
    }
}
