using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pocus.Application.Dto;
using Pocus.Application.Interfaces;
using Pocus.Core.Entities;
using Pocus.Infrastructure.Data;

namespace Pocus.Application.Services;

internal class NoteService(ILogger<NoteService> logger,
                               PocusDbContext dbContext,
                               IMapper mapper) : INoteService
{
    private readonly ILogger<NoteService> _logger = logger;
    private readonly PocusDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task Create(NoteAddDto request)
    {
        try
        {
            await _dbContext.Notes.AddAsync(_mapper.Map<Note>(request));
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"New Note created");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error adding Note: {ex.Message}");
            throw;
        }
    }
    public async Task Copy(int objectId)
    {
        try
        {
            var noteToCopy = await _dbContext.Notes
                .Where(x => x.Id == objectId)
                .FirstOrDefaultAsync();

            if (noteToCopy != null)
            {
                _dbContext.Notes.Add(new Note
                {
                    UserId = noteToCopy.UserId,
                    Title = noteToCopy.Title,
                    Text = noteToCopy.Text,
                    IsArchived = noteToCopy.IsArchived,
                    IsPinned = noteToCopy.IsPinned
                });

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation($"Note copied");
            }
            else
            {
                _logger.LogError($"Note with id {objectId} not found.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error Note copy: {ex.Message}");
            throw;
        }
    }
    public async Task<NoteViewDto> GetById(int noteId)
    {
        var note = await _dbContext.Notes
            .Where(x => x.Id == noteId)
            .FirstAsync();

        return _mapper.Map<NoteViewDto>(note);
    }
    public async Task<List<NoteDto>> GetArchived(string userId)
    {
        var userNotes = await _dbContext.Notes
                    .Where(x => x.UserId == userId &&
                           x.IsArchived &&
                           x.Title != "Habits")
                    .ToListAsync();

        return _mapper.Map<List<NoteDto>>(userNotes);
    }
    public async Task<List<NoteDto>> GetNotArchived(string userId)
    {
        var userNotes = await _dbContext.Notes
                    .Where(x => x.UserId == userId &&
                                !x.IsArchived &&
                                x.Title != "Habits")
                    .ToListAsync();

        return _mapper.Map<List<NoteDto>>(userNotes);
    }
    public async Task<List<ObjectTitleDto>> GetTitlesOfNotArchived(string userId)
    {
        var userNotes = await _dbContext.Notes
            .Where(x => x.UserId == userId &&
                        !x.IsArchived)
            .ToListAsync();

        return _mapper.Map<List<ObjectTitleDto>>(userNotes);
    }
    public async Task Update(NoteUpdateDto request)
    {
        try
        {
            var note = await _dbContext.Notes
                .Where(x => x.Id == request.Id)
                .FirstAsync();

            note.Title = request.Title;
            note.Text = request.Text;
            note.IsPinned = request.IsPinned;

            _dbContext.Notes.Update(note);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Note data updated");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error Note data update: {ex.Message}");
            throw;
        }
    }
    public async Task UpdateText(ObjectUpdateTextDto request)
    {
        try
        {
            var note = await _dbContext.Notes
                .Where(x => x.Id == request.Id)
                .FirstAsync();

            note.Text = request.Text;

            _dbContext.Notes.Update(note);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Note data updated");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error Note data update: {ex.Message}");
            throw;
        }
    }
    public async Task UpdatePinnedStatus(int objectId)
    {
        try
        {
            var note = await _dbContext.Notes
                .Where(x => x.Id == objectId)
                .FirstAsync();

            note.IsPinned = !note.IsPinned;

            _dbContext.Notes.Update(note);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Note pin status updated");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error Note pin status update: {ex.Message}");
            throw;
        }
    }
    public async Task UpdateArchivedStatus(int objectId)
    {
        try
        {
            var note = await _dbContext.Notes
                            .Where(x => x.Id == objectId)
                            .FirstAsync();

            note.IsArchived = !note.IsArchived;

            _dbContext.Notes.Update(note);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Note archived");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error Note archive: {ex.Message}");
            throw;
        }
    }
    public async Task Delete(int objectId)
    {
        try
        {
            var itemToDelete = await _dbContext.Notes
                .FindAsync(objectId) ?? throw new InvalidOperationException("Note not found");

            _dbContext.Notes.Remove(itemToDelete);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Note deleted");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error Note delete: {ex.Message}");
        }
    }
}
