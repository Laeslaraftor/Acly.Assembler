namespace System
{
    public struct HashCode
    {
        private int _value;

        #region Управление

        public void Add<T>(T obj)
        {
            if (obj == null)
            {
                return;
            }

            _value += obj.GetHashCode();
        }
        public readonly int ToHashCode()
        {
            return _value;
        }

        #endregion

        #region Операторы

        public static bool operator ==(HashCode l, HashCode r)
        {
            return l._value == r._value;
        }
        public static bool operator !=(HashCode l, HashCode r)
        {
            return l._value == r._value;
        }
        public static implicit operator int(HashCode hashCode)
        {
            return hashCode._value;
        }

        #endregion

        #region Дополнительно

        public readonly override bool Equals(object? obj)
        {
            return obj is HashCode hashCode
                && hashCode._value == _value;
        }
        public readonly override string ToString()
        {
            return _value.ToString();
        }
        public readonly override int GetHashCode()
        {
            return _value;
        }

        #endregion

        #region Статика

        public static int Combine<T1>(T1 value1)
        {
            if (value1 == null)
            {
                return 0;
            }

            return value1.GetHashCode();
        }
        public static int Combine<T1, T2>(T1 value1, T2 value2)
        {
            int result = 0;

            if (value1 != null)
            {
                result += value1.GetHashCode();
            }
            if (value2 != null)
            {
                result += value2.GetHashCode();
            }

            return result;
        }
        public static int Combine<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        {
            int result = 0;

            if (value1 != null)
            {
                result += value1.GetHashCode();
            }
            if (value2 != null)
            {
                result += value2.GetHashCode();
            }
            if (value3 != null)
            {
                result += value3.GetHashCode();
            }

            return result;
        }
        public static int Combine<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            int result = 0;

            if (value1 != null)
            {
                result += value1.GetHashCode();
            }
            if (value2 != null)
            {
                result += value2.GetHashCode();
            }
            if (value3 != null)
            {
                result += value3.GetHashCode();
            }
            if (value4 != null)
            {
                result += value4.GetHashCode();
            }

            return result;
        }
        public static int Combine<T1, T2, T3, T4, T5>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
        {
            int result = 0;

            if (value1 != null)
            {
                result += value1.GetHashCode();
            }
            if (value2 != null)
            {
                result += value2.GetHashCode();
            }
            if (value3 != null)
            {
                result += value3.GetHashCode();
            }
            if (value4 != null)
            {
                result += value4.GetHashCode();
            }
            if (value5 != null)
            {
                result += value5.GetHashCode();
            }

            return result;
        }
        public static int Combine<T1, T2, T3, T4, T5, T6>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6)
        {
            int result = 0;

            if (value1 != null)
            {
                result += value1.GetHashCode();
            }
            if (value2 != null)
            {
                result += value2.GetHashCode();
            }
            if (value3 != null)
            {
                result += value3.GetHashCode();
            }
            if (value4 != null)
            {
                result += value4.GetHashCode();
            }
            if (value5 != null)
            {
                result += value5.GetHashCode();
            }
            if (value6 != null)
            {
                result += value6.GetHashCode();
            }

            return result;
        }
        public static int Combine<T1, T2, T3, T4, T5, T6, T7>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7)
        {
            int result = 0;

            if (value1 != null)
            {
                result += value1.GetHashCode();
            }
            if (value2 != null)
            {
                result += value2.GetHashCode();
            }
            if (value3 != null)
            {
                result += value3.GetHashCode();
            }
            if (value4 != null)
            {
                result += value4.GetHashCode();
            }
            if (value5 != null)
            {
                result += value5.GetHashCode();
            }
            if (value6 != null)
            {
                result += value6.GetHashCode();
            }
            if (value7 != null)
            {
                result += value7.GetHashCode();
            }

            return result;
        }
        public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8)
        {
            int result = 0;

            if (value1 != null)
            {
                result += value1.GetHashCode();
            }
            if (value2 != null)
            {
                result += value2.GetHashCode();
            }
            if (value3 != null)
            {
                result += value3.GetHashCode();
            }
            if (value4 != null)
            {
                result += value4.GetHashCode();
            }
            if (value5 != null)
            {
                result += value5.GetHashCode();
            }
            if (value6 != null)
            {
                result += value6.GetHashCode();
            }
            if (value7 != null)
            {
                result += value7.GetHashCode();
            }
            if (value8 != null)
            {
                result += value8.GetHashCode();
            }

            return result;
        }


        #endregion
    }
}
