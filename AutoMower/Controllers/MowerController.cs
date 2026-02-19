using AutoMower.Core;
using AutoMower.Core.Interfaces;
using AutoMower.Core.Parser;
using AutoMower.Core.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AutoMower.Controllers;

[ApiController]
[Route("[controller]")]
public class MowerController : ControllerBase
{
    private readonly IInputParse _inputParse;
    private readonly IMowerService _mowerService;
    private readonly ILogger<MowerController> _logger;

    public MowerController(IInputParse inputParse, IMowerService mowerService, ILogger<MowerController> logger)
    {
        this._inputParse = inputParse;
        this._mowerService = mowerService;
        _logger = logger;
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> RunMowers([Required] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Fichier invalide.");

        string content;

        using (var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
        {
            content = await reader.ReadToEndAsync();
        }

        var lines = content
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        try
        {
            var resultParse = _inputParse.Parse(lines);

            var results = _mowerService.Execute(new Core.Commands.MowerCommand(resultParse.Item1, resultParse.Item2));

            var output = results
                .Select(p => $"{p.X} {p.Y} {p.Orientation}")
                .ToList();

            return Ok(output);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Erreur de validation des données d'entrée");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'exécution des tondeuses");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
