using System.Reflection;

namespace System
{
    public class Object
    {
        public Type GetType()
        {
            throw new NotImplementedException();
        }
        public virtual bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            return GetHashCode() == obj.GetHashCode();
        }
        public virtual string ToString()
        {
            return GetType().Name;
        }
        public virtual int GetHashCode()
        {
            return 0;
        }
    }
}
