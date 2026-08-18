# PostBoard

A lightweight .NET 8 Razor Pages application that reads and creates posts through the [JSONPlaceholder API](https://jsonplaceholder.typicode.com/).

## Features

- Browse all 100 posts in a responsive card layout
- Filter posts instantly by title or content
- Open a dedicated post detail page
- Create a post with client- and server-side validation
- Friendly loading and submission error states
- Responsive Bootstrap-based interface with a small custom design layer

## Running locally

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- An internet connection (the app calls JSONPlaceholder and loads Bootstrap assets from jsDelivr)

### Start the application

```bash
git clone https://github.com/NathanHatchy/postboard-dotnet8.git
cd postboard-dotnet8
dotnet restore
dotnet run --project PostBoard
```

Open the URL printed in the terminal (by default `http://localhost:5182`).

You can also build the whole solution with:

```bash
dotnet build PostBoard.sln
```

## Approach

Razor Pages keeps this small, server-rendered assignment simple: no separate JavaScript build chain and very little ceremony. API access sits behind `IPostService`, keeping HTTP and JSON concerns out of the page models and making the boundary straightforward to replace or test. A typed `HttpClient` provides central configuration, connection reuse and a clear place for future resilience policies.

The pages handle expected API failures without exposing technical details to users. Input is validated on both client and server, while the service calls use cancellation tokens and verify HTTP status codes.

## JSONPlaceholder behavior

JSONPlaceholder simulates writes. A `POST /posts` request returns a successful response (normally with ID `101`), but the new post is **not persisted** and will not appear in a later `GET /posts` response. The confirmation page calls this out rather than pretending the data has been stored.

## Project structure

```text
PostBoard/
├── Models/          Post model and validation rules
├── Services/        API abstraction and JSONPlaceholder client
├── Pages/           Razor Pages and page models
└── wwwroot/css/     Small custom visual layer
```

## Trade-offs and next steps

The scope intentionally prioritises polished Read and Create flows. Given more time, I would:

- Add automated unit tests around the API client using a mocked `HttpMessageHandler`, plus browser-level happy-path tests
- Add Update and Delete while clearly retaining the mock-API disclaimer
- Move the API base URL into configuration for different environments
- Add retry/circuit-breaker policies and structured observability for a production service
- Bundle front-end dependencies locally or through an asset pipeline instead of relying on a CDN
- Add server-side pagination if the backing API grew beyond this 100-item demo
- Set limit on page view as 100 posts is excessive, approx 30 would suffice
- Use Sass instead of Css for frontend flexibility

## Technology

- .NET 8 / ASP.NET Core Razor Pages
- Typed `HttpClient` and `System.Net.Http.Json`
- Bootstrap 5.3 with custom CSS
- JSONPlaceholder REST API
