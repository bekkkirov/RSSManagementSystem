using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSS.Application.Interfaces.Services;
using RSS.Application.Models;

namespace RSS.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FeedsController : ControllerBase
{
    private readonly IChannelService _channelService;

    public FeedsController(IChannelService channelService)
    {
        _channelService = channelService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChannelDto>>> Get()
    {
        var channels = await _channelService.GetAllAsync();

        return Ok(channels);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ChannelDto>> GetById(int id)
    {
        var channel = await _channelService.GetByIdAsync(id);

        return Ok(channel);
    }

    [HttpPost]
    public async Task<ActionResult<ChannelDto>> Add([FromBody] string feedUrl)
    {
        var created = await _channelService.AddAsync(feedUrl);

       return CreatedAtAction(nameof(GetById), new { Id = created.Id }, created);
    }
}