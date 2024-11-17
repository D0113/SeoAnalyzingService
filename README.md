# SEO Analyzing Service (SEOAS)

SEOAS is a .NET API for analyzing SEO metrics based on search key words and results returned by GG search and Bing.
This service returns a string that contains number of times and positions where a website(url) appears in the search result compatible to each key word input.

## Table of Contents
- [SEO Analyzing Service](#seo-analyzing-service)
  - [Table of Contents](#table-of-contents)
  - [Features](#features)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Usage](#usage)
  - [API Endpoints](#api-endpoints)
  - [Environment Variables](#environment-variables)
  - [Contributing](#contributing)
  - [License](#license)
  - [Contact](#contact)

## Features

- Analyze SEO metrics for given URLs and keywords.
- Supports multiple search engines (Google, Bing).

## Prerequisites

- .NET 8.0 SDK

## Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/D0113/SeoAnalyzingService.git
    cd SeoAnalyzingService/SeoAnalyzing.WebService
    ```

2. Restore the NuGet packages:
    ```bash
    dotnet restore
    ```
    
3. Build the project:
    ```bash
    dotnet build
    ```

4. Run the service:
    ```bash
    dotnet run
    ```

## Usage

1. Start the API server by running the service.
2. Access the API documentation at `http://localhost:5130/swagger` to explore the available endpoints and their usage.

## API Endpoints

Here are some example endpoints:

- **Analyze Bing SEO**:
    ```http
    GET /api/v1/BingAnalyzing/Search?searchQuery={searchQuery}&searchUrl={searchUrl}&searchLimit={searchLimit}
    ```

- **Analyze Google SEO**:
    ```http
    GET /api/v1/GoogleAnalyzing/Search?searchQuery={searchQuery}&searchUrl={searchUrl}&searchLimit={searchLimit}
    ```

## Environment Variables

Not implemented yet!

## Contributing

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-name`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature-name`).
5. Open a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact

For any queries or suggestions, please contact:

- [doduc017@gmail.com](doduc017@gmail.com)
- GitHub: [D0113](https://github.com/D0113)

