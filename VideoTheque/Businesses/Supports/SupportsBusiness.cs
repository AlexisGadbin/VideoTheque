using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.Supports;

namespace VideoTheque.Businesses.Supports
{
    public class SupportsBusiness : ISupportsBusiness
    {
        private readonly ISupportsRepository _supportRepository;

        public SupportsBusiness(ISupportsRepository supportRepository)
        {
            _supportRepository = supportRepository;
        }

        public List<SupportDto> GetSupports()
        {
            return _supportRepository.GetSupports();
        }
        public SupportDto GetSupport(int id)
        {
            var support = _supportRepository.GetSupport(id);

            if (support is null)
            {
                throw new NotFoundException($"Support with id {id} not found");
            }

            return support;
        }
    }
}
