namespace Helper.Datatypes
{
    using System;
    using System.Reflection;
    using Xamarin.Forms.Internals;

    public static class StringEnum
    {
        public static string GetStringValue(Enum value)
        {
            string output = null;
            var type = value.GetType();

            var attrs =
                type.GetField(value.ToString()).GetCustomAttributes(typeof(StringValue),
                    false) as StringValue[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }
}