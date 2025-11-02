using Microsoft.AspNetCore.Mvc;
using PokerHands.Api.Models;
using PokerHands.Core.Exceptions;
using PokerHands.Core.Interfaces;
using PokerHands.Core.Utils;

namespace PokerHands.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokerController(IEvaluatorService evaluator, IDeckService deck, ILogger<PokerController> logger) : ControllerBase
    {
        /// <summary>
        /// Generates a random poker hand.
        /// </summary>
        [HttpGet("generate")]
        public IActionResult GenerateHand()
        {
            var hand = deck.DealHand();

            logger.LogInformation("Generated new hand: {Cards}", string.Join(", ", hand.Cards));

            return Ok(new
            {
                CardsNames = hand.ToString()
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
                var hand = HandParser.ParseHand(cards);
                var evaluation = evaluator.Evaluate(hand);

                logger.LogInformation("Evaluated hand: {Cards} -> {Hand Rank}",
                    string.Join(", ", cards), evaluation.HandRank);

                return Ok(new
                {
                    HandRank = evaluation.HandRank.ToString(),
                    Tiebreakers = evaluation.Tiebreakers.Select(t => t.ToString())
                });
            }
            catch (InvalidCardException ex)
            {
                logger.LogWarning(ex, "Invalid hand provided: {Cards}", string.Join(", ", cards));

                return BadRequest("The hand contains invalid cards.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error evaluating hand");

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
                request.HandA?.Count != 5 ||
                request.HandB?.Count != 5)
            {
                return BadRequest("Both hands must have exactly 5 cards.");
            }

            try
            {
                var handA = HandParser.ParseHand(request.HandA);
                var handB = HandParser.ParseHand(request.HandB);

                var evalA = evaluator.Evaluate(handA);
                var evalB = evaluator.Evaluate(handB);

                int result = evaluator.CompareHands(handA, handB);

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

                logger.LogInformation(
                    "Compared hands: A={HandA}, B={HandB}, Winner={Winner}",
                    string.Join(", ", request.HandA),
                    string.Join(", ", request.HandB),
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
                logger.LogWarning(ex, "Invalid hands provided: A={HandA}, B={HandB}",
                    string.Join(", ", request.HandA),
                    string.Join(", ", request.HandB));
             
                return BadRequest("One or both hands contain invalid cards.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error comparing hands");
                
                return StatusCode(500, "An unexpected error occurred while comparing the hands.");
            }
        }
    }
}