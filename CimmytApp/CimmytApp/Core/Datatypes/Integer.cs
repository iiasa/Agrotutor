namespace Helper.Datatypes
{
    public class Integer
    {
        private readonly int _value;

        public Integer(int value)
        {
            _value = value;
        }

        public static int operator +(Integer one, Integer two)
        {
            return one._value + two._value;
        }

        public static Integer operator +(int one, Integer two)
        {
            return new Integer(one + two);
        }

        public static implicit operator Integer(int value)
        {
            return new Integer(value);
        }

        public static implicit operator int(Integer integer)
        {
            return integer._value;
        }

        public static int operator -(Integer one, Integer two)
        {
            return one._value - two._value;
        }

        public static Integer operator -(int one, Integer two)
        {
            return new Integer(one - two);
        }

        public int getValue()
        {
            return _value;
        }
    }
}