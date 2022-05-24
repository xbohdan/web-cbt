using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCbt_Backend.Data;
using WebCbt_Backend.Models;

namespace WebCbt_Backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class EvaluationController : ControllerBase
    {
        private readonly WebCbtDbContext _context;

        public EvaluationController(WebCbtDbContext context)
        {
            _context = context;
        }

        // POST: /moodtests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("~/moodtests")]
        public async Task<ActionResult<Evaluation>> PostEvaluation(Evaluation evaluation)
        {
            if (User.Identity?.IsAuthenticated != true || User.FindFirstValue("userId") != evaluation.UserId.ToString())
            {
                return Unauthorized();
            }

            if (_context.Evaluations == null)
            {
                return Problem("Entity set is null.");
            }

            _context.Evaluations.Add(evaluation);

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: /evaluation/{evaluationId}
        [HttpDelete("{evaluationId}")]
        public async Task<IActionResult> DeleteEvaluation(int evaluationId)
        {
            if (User.Identity?.IsAuthenticated != true)
            {
                return Unauthorized();
            }

            if (_context.Evaluations == null)
            {
                return NotFound();
            }

            var evaluation = await _context.Evaluations.FindAsync(evaluationId);

            if (evaluation == null)
            {
                return NotFound();
            }

            if (User.FindFirstValue("userId") != evaluation.UserId.ToString())
            {
                return Unauthorized();
            }

            _context.Evaluations.Remove(evaluation);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
