namespace Helper.Datatypes
{
    public class Integer
    {
        private readonly int value;

        public Integer(int value)
        {
            this.value = value;
        }

        public static int operator +(Integer one, Integer two)
        {
            return one.value + two.value;
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
            return integer.value;
        }

        public static int operator -(Integer one, Integer two)
        {
            return one.value - two.value;
        }

        public static Integer operator -(int one, Integer two)
        {
            return new Integer(one - two);
        }

        public int getValue()
        {
            return value;
        }
    }
}