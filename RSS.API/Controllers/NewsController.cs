using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSS.Application.Interfaces.Services;
using RSS.Application.Models;

namespace RSS.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpGet("{date}")]
    public async Task<ActionResult<IEnumerable<FeedItemDto>>> GetByDate(DateTime date)
    {
        var news = await _newsService.GetUnreadByDateAsync(date);

        return Ok(news);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> MarkAsRead(int id)
    {
        await _newsService.MarkAsReadAsync(id);

        return Ok();
    }
}