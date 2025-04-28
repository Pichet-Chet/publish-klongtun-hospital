using System;
namespace KTH.REPOSITORIES
{
    public static class Helper
    {
        public static string GenerateShortGuid()
        {
            Guid guid = Guid.NewGuid();

            string shortGuid = guid.ToString().Replace("-", "").Substring(0, 6);

            return shortGuid;
        }

        public static Guid ToGuid(this string value)
        {
            return new Guid(value);
        }

        public static short ToShort(this object value)
        {
            try
            {
                return short.Parse(value.ToString());
            }
            catch
            {
                return 0;
            }
        }
    }

    public static class ConstantsData
    {
        public const string Undefined = "undefined";
    }
}

