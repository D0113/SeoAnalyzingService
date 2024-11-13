﻿using SeoAnalyzing.Model.Search;

namespace SeoAnalyzing.UnitTest.MockData
{
    public class SearchResponseModelMocks
    {
        public static SearchResponseModel GenerateResponseModel(string searchEngine, string position = "1, 2, 3", int total = 3)
        {
            return new SearchResponseModel
            {
                TotalCount = total,
                Position = position,
                SearchEngine = searchEngine
            };
        }

        public static SearchResponseModel GenerateResponseModelDefault(string searchEngine)
        {
            return new SearchResponseModel
            {
                TotalCount = 0,
                Position = "0",
                SearchEngine = searchEngine
            };
        }
    }
}