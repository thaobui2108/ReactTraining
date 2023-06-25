using System.Collections;

namespace WebReactApi.Helper
{
    public class Guard
    {
        public static void ArgumentNotNull(string argName, object argValue)
        {
            if (argValue == null)
                throw new ArgumentNullException(argName);
        }

        public static void ArgumentNotNullOrEmpty(string argName, IEnumerable argValue)
        {
            ArgumentNotNull(argName, argValue);

            if (!argValue.GetEnumerator().MoveNext())
                throw new ArgumentException("Argument was empty", argName);
        }

        public static void ArgumentValid(string argName, string message, bool test)
        {
            if (!test)
                throw new ArgumentException(message, argName);
        }
    }
}
