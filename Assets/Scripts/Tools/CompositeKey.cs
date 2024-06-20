using System.Collections.Generic;

namespace Deege.Tools
{
    public class CompositeKey<TKey1, TKey2>
    {
        public TKey1 Key1 { get; }
        public TKey2 Key2 { get; }

        public CompositeKey(TKey1 key1, TKey2 key2)
        {
            Key1 = key1;
            Key2 = key2;
        }

        public override bool Equals(object obj)
        {
            if (obj is CompositeKey<TKey1, TKey2> other)
            {
                return EqualityComparer<TKey1>.Default.Equals(Key1, other.Key1) &&
                       EqualityComparer<TKey2>.Default.Equals(Key2, other.Key2);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Key1, Key2);
        }
    }
}