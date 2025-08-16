namespace System
{
    public struct Int32
    {
        public Int32(int value)
        {
            _value = value;
        }

        private int _value;

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
