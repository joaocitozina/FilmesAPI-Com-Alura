using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{

    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaFilme(
        [FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaFilmePorId),
            new { id = filme.Id },
            filme);
    }

    /// <summary>
    /// Retorna uma lista de todos os filmes cadastrados.
    /// </summary>
    /// <returns>Lista de objetos FilmeDto.</returns>
    /// <response code="200">Retorna a lista de filmes com sucesso.</response>

    [HttpGet]
    public IEnumerable<ReadFilmeDto> RecuperaFilmes
        ([FromQuery] int skip = 0,
        [FromQuery] int take = 50,
        [FromQuery] string? nomeCinema = null)
    {
        if(nomeCinema == null)
        {
            return _mapper.Map<List<ReadFilmeDto>>
                (_context.Filmes.Skip(skip).Take(take).ToList());
        }
        return _mapper.Map<List<ReadFilmeDto>>
                (_context.Filmes.Skip(skip).Take(take).Where(filme => filme.Sessoes
                .Any(sessao => sessao.Cinema.Nome == nomeCinema)).ToList());
    }


    /// <summary>
    /// Busca um filme específico pelo seu identificador (ID).
    /// </summary>
    /// <param name="id">O ID do filme a ser buscado.</param>
    /// <returns>O objeto FilmeDto correspondente ao ID.</returns>
    /// <response code="200">Retorna o filme solicitado.</response>
    /// <response code="404">Se o filme com o ID especificado não for encontrado.</response>

    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
        var filme = _context.Filmes
            .FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        return Ok(filmeDto);
    }

    /// <summary>
    /// Atualiza completamente os dados de um filme existente.
    /// </summary>
    /// <param name="id">O ID do filme a ser atualizado.</param>
    /// <param name="filmeDto">O objeto FilmeDto com os novos dados.</param>
    /// <returns>Um status de NoContent se a atualização for bem-sucedida.</returns>
    /// <response code="204">Atualização realizada com sucesso (Sem Conteúdo).</response>
    /// <response code="400">Dados inválidos ou ID na rota não confere com o objeto.</response>
    /// <response code="404">Se o filme com o ID especificado não for encontrado.</response>

    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(int id,
        [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(
            filme => filme.Id == id);
        if (filme == null) return NotFound();
        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Atualiza parcialmente os dados de um filme existente (ex: apenas o título ou a nota).
    /// </summary>
    /// <param name="id">O ID do filme a ser parcialmente atualizado.</param>
    /// <param name="patchDocument">Documento JSON Patch (RFC 6902) com as operações de atualização.</param>
    /// <returns>Um status de NoContent se a atualização for bem-sucedida.</returns>
    /// <response code="204">Atualização parcial realizada com sucesso.</response>
    /// <response code="400">Dados inválidos ou formato do patch incorreto.</response>
    /// <response code="404">Se o filme com o ID especificado não for encontrado.</response>

    [HttpPatch("{id}")]
    public IActionResult AtualizaFilmeParcial(int id,
        JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(
            filme => filme.Id == id);
        if (filme == null) return NotFound();

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();
    }


    /// <summary>
    /// Remove um filme do banco de dados pelo seu ID.
    /// </summary>
    /// <param name="id">O ID do filme a ser removido.</param>
    /// <returns>Um status de NoContent se a exclusão for bem-sucedida.</returns>
    /// <response code="204">Exclusão realizada com sucesso (Sem Conteúdo).</response>
    /// <response code="404">Se o filme com o ID especificado não for encontrado.</response>

    [HttpDelete("{id}")]
    public IActionResult DeletaFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(
            filme => filme.Id == id);
        if (filme == null) return NotFound();
        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}
        