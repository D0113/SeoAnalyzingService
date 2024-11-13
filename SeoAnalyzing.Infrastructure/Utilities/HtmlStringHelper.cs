using System.Text.RegularExpressions;

namespace SeoAnalyzing.Infrastructure.Utilities
{
    public static class HtmlStringHelper
    {
        public static IEnumerable<int> GetPositions(string html, string url, string urlPattern, int limit)
        {
            var positions = new List<int>();
            var regex = new Regex(urlPattern, RegexOptions.IgnoreCase);
            var matches = regex.Matches(html).Take(limit);
            int index = 1;

            foreach (Match match in matches)
            {
                if (match.ToString().Contains(url, StringComparison.OrdinalIgnoreCase))
                {
                    positions.Add(index);
                }

                index++;
            }

            return positions;
        }
    }
}
