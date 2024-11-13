using SeoAnalyzing.Common.Constants;
using SeoAnalyzing.Common.Validations;
using System.ComponentModel.DataAnnotations;

namespace SeoAnalyzing.Model.Search
{
    public class SearchRequestModel
    {
        [Required]
        public string SearchQuery { get; set; }

        [Required]
        [CustomUrlValidation]
        public string SearchUrl { get; set; }

        [DivisibleByTen(ValidationConstants.SearchLimitDefaultValue)]
        public int SearchLimit { get; set; } = ValidationConstants.SearchLimitDefaultValue;
    }
}
