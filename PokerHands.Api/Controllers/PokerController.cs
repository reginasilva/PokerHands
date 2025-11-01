using Microsoft.AspNetCore.Mvc;
using PokerHands.Core.Exceptions;
using PokerHands.Core.Interfaces;
using PokerHands.Core.Models;
using PokerHands.Core.Services;
using PokerHands.Core.Utils;

namespace PokerHands.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokerController : ControllerBase
    {
        private readonly IEvaluatorService _evaluator;
        private readonly IDeckService _deck;
        private readonly ILogger<PokerController> _logger;

        public PokerController(IEvaluatorService evaluator, IDeckService deck, ILogger<PokerController> logger)
        {
            _evaluator = evaluator;
            _deck = deck;
            _logger = logger;
        }

        /// <summary>
        /// Generates a random poker hand.
        /// </summary>
        [HttpGet("generate")]
        public IActionResult GenerateHand()
        {
            var hand = _deck.DealHand();

            _logger.LogInformation("Generated new hand: {Cards}", string.Join(", ", hand.Cards));

            return Ok(new
            {
                Cards = hand.Cards.Select(c => new
                {
                    Rank = c.Rank.ToString(),
                    Suit = c.Suit.ToString()
                })
            });
        }

        /// <summary>
        /// Evaluates a single hand and returns its hand rank and tiebreakers.
        /// </summary>
        [HttpPost("evaluate")]
        public IActionResult EvaluateHand([FromBody] List<string> cards)
        {
            if (cards == null ||
                cards.Count != 5)
            {
                return BadRequest("A valid hand with exactly 5 cards must be provided.");
            }

            try
            {
                var hand = CardParser.ParseHand(cards);
                var evaluation = _evaluator.Evaluate(hand);

                _logger.LogInformation("Evaluated hand: {Cards} -> {Hand Rank}",
                    string.Join(", ", cards), evaluation.HandRank);

                return Ok(new
                {
                    HandRank = evaluation.HandRank.ToString(),
                    Tiebreakers = evaluation.Tiebreakers.Select(t => t.ToString())
                });
            }
            catch (InvalidCardException ex)
            {
                _logger.LogWarning(ex, "Invalid hand provided: {Cards}", string.Join(", ", cards));

                return BadRequest("The hand contains invalid cards.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error evaluating hand");

                return StatusCode(500, "An unexpected error occurred while evaluating the hand.");
            }
        }

        /// <summary>
        /// Compares two poker hands and returns which one wins.
        /// </summary>
        [HttpPost("compare")]
        public IActionResult CompareHands([FromBody] CompareRequest request)
        {
            if (request == null ||
                request.Hand1?.Count != 5 ||
                request.Hand2?.Count != 5)
            {
                return BadRequest("Both hands must have exactly 5 cards.");
            }

            try
            {
                var handA = CardParser.ParseHand(request.Hand1);
                var handB = CardParser.ParseHand(request.Hand2);

                var evalA = _evaluator.Evaluate(handA);
                var evalB = _evaluator.Evaluate(handB);

                int result = _evaluator.CompareHands(handA, handB);

                string winner = result switch
                {
                    > 0 => "Hand A",
                    < 0 => "Hand B",
                    _ => "Tie"
                };

                string reason = result switch
                {
                    > 0 => $"Hand A wins: {evalA.HandRank} beats {evalB.HandRank}",
                    < 0 => $"Hand B wins: {evalB.HandRank} beats {evalA.HandRank}",
                    _ => $"Both hands are equal ({evalA.HandRank})"
                };

                _logger.LogInformation(
                    "Compared hands: A={HandA}, B={HandB}, Winner={Winner}",
                    string.Join(", ", request.Hand1),
                    string.Join(", ", request.Hand2),
                    winner);

                return Ok(new
                {
                    Winner = winner,
                    Reason = reason,
                    HandARank = evalA.HandRank.ToString(),
                    HandBRank = evalB.HandRank.ToString(),
                    Comparison = result
                });
            }
            catch (InvalidCardException ex)
            {
                _logger.LogWarning(ex, "Invalid hands provided: A={HandA}, B={HandB}",
                    string.Join(", ", request.Hand1),
                    string.Join(", ", request.Hand2));
             
                return BadRequest("One or both hands contain invalid cards.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error comparing hands");
                
                return StatusCode(500, "An unexpected error occurred while comparing the hands.");
            }
        }
    }
}