using Microsoft.AspNetCore.Mvc;
using SeoAnalyzing.Infrastructure.Interfaces.Services;
using SeoAnalyzing.Model.Search;

namespace SeoAnalyzing.WebService.Controllers
{
    [ApiController]
    public class GoogleAnalyzingController : ApiBaseController
    {
        private readonly IGoogleSearchService _googleSearchService;

        public GoogleAnalyzingController(IGoogleSearchService googleSearchService)
        {
            _googleSearchService = googleSearchService;
        }

        [HttpPost]
        [Route("Search")]
        public async Task<IActionResult> SearchAsync([FromQuery] SearchRequestModel request)
        {
            var result = await _googleSearchService.SearchAsync(request);
            return Ok(result);
        }
    }
}
