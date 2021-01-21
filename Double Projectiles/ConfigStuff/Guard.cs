using System;

namespace ConfigHelper
{
    internal class Guard
    {
        public static void ThrowIfArgumentIsNull(object obj, string argumentName, string message = "")
        {
            bool flag = obj != null;
            if (flag)
            {
                return;
            }
            bool flag2 = string.IsNullOrEmpty(message);
            if (flag2)
            {
                throw new ArgumentNullException(argumentName);
            }
            throw new ArgumentNullException(argumentName, message);
        }
        public static void ThrowIfStringIsNull(string stringToCheck, string message)
        {
            bool flag = string.IsNullOrEmpty(stringToCheck);
            if (flag)
            {
                throw new Exception(message);
            }
        }
    }
}
