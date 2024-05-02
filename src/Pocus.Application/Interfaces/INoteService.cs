﻿using Pocus.Application.Dto;

namespace Pocus.Application.Interfaces;

public interface INoteService
{
    public Task Create(NoteDto request);
    public Task<NoteViewDto?> GetById(int noteId);
    public Task<List<NoteDto>?> GetArchived(int userId);
    public Task<List<NoteDto>?> GetNotArchived(int userId);
    public Task Update(NoteUpdateDto request);
}