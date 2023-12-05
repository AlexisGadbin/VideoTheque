using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Supports
{
    public interface ISupportsRepository
    {
        List<SupportDto> GetSupports();
        SupportDto GetSupport(int id);
    }
}
