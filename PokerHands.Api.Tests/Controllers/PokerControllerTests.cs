using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PokerHands.Api.Models;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace PokerHands.Api.Tests.Controllers
{
    public class PokerControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task GenerateHand_ShouldReturnFiveCards()
        {
            var response = await _client.GetAsync("/api/poker/generate");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadFromJsonAsync<JsonElement>();

            content.TryGetProperty("cardsNames", out var cardsNamesProp).Should().BeTrue();
            
            var cardsNames = cardsNamesProp.GetString();

            cardsNames.Should().NotBeNullOrWhiteSpace();
            cardsNames!.Split(',').Length.Should().Be(5);

            cardsNames.Should().Contain("of");
        }

        [Fact]
        public async Task EvaluateHand_ShouldReturnRoyalFlush()
        {
            var hand = new[] { "AS", "KS", "QS", "JS", "10S" };

            var response = await _client.PostAsJsonAsync("/api/poker/evaluate", hand);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadFromJsonAsync<JsonElement>();

            content.GetProperty("handRank").GetString().Should().Be("RoyalFlush");
        }

        [Fact]
        public async Task EvaluateHand_ShouldHandleLowerCaseInput()
        {
            var hand = new[] { "as", "ks", "qs", "js", "10s" };

            var response = await _client.PostAsJsonAsync("/api/poker/evaluate", hand);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();

            json.GetProperty("handRank").GetString().Should().Be("RoyalFlush");
        }

        [Fact]
        public async Task EvaluateHand_ShouldReturnBadRequest_WhenInvalidCardList()
        {
            var invalidHand = new[] { "AS", "KS" }; 
            var response = await _client.PostAsJsonAsync("/api/poker/evaluate", invalidHand);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CompareHands_ShouldDeclareHandAAsWinner()
        {
            var request = new CompareRequest
            {
                Hand1 = ["AS", "KS", "QS", "JS", "10S"],
                Hand2 = ["9C", "9D", "9H", "9S", "2C"]
            };

            var response = await _client.PostAsJsonAsync("/api/poker/compare", request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<JsonElement>();

            result.GetProperty("winner").GetString().Should().Be("Hand A");
            result.GetProperty("handARank").GetString().Should().Be("RoyalFlush");
            result.GetProperty("handBRank").GetString().Should().Be("FourOfKind");
        }

        [Fact]
        public async Task CompareHands_ShouldReturnTie_WhenHandsAreEqual()
        {
            var request = new CompareRequest
            {
                Hand1 = ["2S", "4D", "6H", "8C", "10S"],
                Hand2 = ["2S", "4D", "6H", "8C", "10S"]
            };

            var response = await _client.PostAsJsonAsync("/api/poker/compare", request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();

            json.GetProperty("winner").GetString().Should().Be("Tie");
            json.GetProperty("comparison").GetInt32().Should().Be(0);
        }

        [Fact]
        public async Task CompareHands_ShouldReturnBadRequest_WhenInvalidHand()
        {
            var request = new CompareRequest
            {
                Hand1 = ["AS", "KS"],
                Hand2 = ["9C", "9D", "9H", "9S", "2C"]
            };

            var response = await _client.PostAsJsonAsync("/api/poker/compare", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
