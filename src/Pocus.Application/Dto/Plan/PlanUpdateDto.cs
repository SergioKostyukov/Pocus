﻿namespace Pocus.Application.Dto;

public class PlanUpdateDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public bool IsPinned { get; set; }
    public DateTime? NotificationTime { get; set; }
}
