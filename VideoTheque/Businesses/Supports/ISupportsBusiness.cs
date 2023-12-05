using VideoTheque.DTOs;

namespace VideoTheque.Businesses.Supports
{
    public interface ISupportsBusiness
    {
        List<SupportDto> GetSupports();
        SupportDto GetSupport(int id);
    }
}
