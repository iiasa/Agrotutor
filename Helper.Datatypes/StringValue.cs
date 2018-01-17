namespace Helper.Datatypes
{
    public class StringValue : System.Attribute
    {
        public StringValue(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}