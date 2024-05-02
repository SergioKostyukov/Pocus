using Pocus.Application.Dto;

namespace Pocus.Application.Interfaces;

public interface IObjectService
{
    public Task Copy(int objectId);
    public Task<List<ObjectTitleDto>?> GetTitlesOfNotArchived(int userId);
    public Task UpdateText(ObjectUpdateTextDto request);
    public Task UpdatePinnedStatus(int objectId);
    public Task UpdateArchivedStatus(int objectId);
    public Task Delete(int objectId);
}
