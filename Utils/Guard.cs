using System;

namespace CodeKata.Utils
{
    public static class Guard
    {
        public static void IsNotNull(object value)
        {
            if(value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
        }
    }
}
