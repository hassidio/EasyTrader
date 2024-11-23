using System.Data;
using System.Globalization;
using System.Text;
using System.Web;

namespace EasyTrader.Core.Common
{
    public static class Helpers
    {
        public static string? BuildQueryString(
             Dictionary<string, string> queryParameters)
        {
            if (queryParameters is null) { return null; }

            StringBuilder builder = new StringBuilder();

            foreach (KeyValuePair<string, string> queryParameter in queryParameters)
            {
                string queryParameterValue =
                    Convert.ToString(queryParameter.Value, CultureInfo.InvariantCulture);

                if (!string.IsNullOrWhiteSpace(queryParameterValue))
                {
                    if (builder.Length > 0)
                    {
                        builder.Append("&");
                    }

                    builder
                        .Append(queryParameter.Key)
                        .Append("=")
                        .Append(HttpUtility.UrlEncode(queryParameterValue));
                }
            }

            return builder.ToString();
        }

        
    }
}