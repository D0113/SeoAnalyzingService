using Microsoft.AspNetCore.Mvc;
using SeoAnalyzing.Infrastructure.Interfaces.Services;
using SeoAnalyzing.Model.Search;

namespace SeoAnalyzing.WebService.Controllers
{
    [ApiController]
    public class BingAnalyzingController : ApiBaseController
    {
        private readonly IBingSearchService _bingSearchService;

        public BingAnalyzingController(IBingSearchService bingSearchService)
        {
            _bingSearchService = bingSearchService;
        }

        [HttpPost]
        [Route("Search")]
        public async Task<IActionResult> SearchAsync([FromQuery] SearchRequestModel request)
        {
            var result = await _bingSearchService.SearchAsync(request);
            return Ok(result);
        }
    }
}
