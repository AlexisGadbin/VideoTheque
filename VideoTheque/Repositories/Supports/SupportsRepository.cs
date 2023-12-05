using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Supports
{
    public class SupportsRepository : ISupportsRepository
    {
        public List<SupportDto> GetSupports()
        {
            List<SupportDto> supports = new List<SupportDto>();

            foreach (SupportType supportType in Enum.GetValues(typeof(SupportType)))
            {
                supports.Add(new SupportDto
                {
                    Id = (int)supportType,
                    Name = supportType.ToString()
                });
            }

            return supports;

        }
        public SupportDto GetSupport(int id)
        {
            return GetSupports().FirstOrDefault(s => s.Id == id);
        }
    }

    public enum SupportType
    {
       BluRay = 1,
       DVD = 2,
    }
}
