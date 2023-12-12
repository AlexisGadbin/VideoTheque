using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.Hosts;

namespace VideoTheque.Businesses.Hosts
{
    public class HostsBusiness : IHostsBusiness
    {
        private readonly IHostsRepository _hostsDao;

        public HostsBusiness(IHostsRepository hostsDao)
        {
            _hostsDao = hostsDao;
        }
        public Task<List<HostDto>> GetHosts()
        {
            return _hostsDao.GetHosts();
        }

        public HostDto GetHost(int id)
        {
            var host = _hostsDao.GetHost(id).Result;

            if(host is null)
            {
                throw new NotFoundException($"Host with id {id} not found");
            }

            return host;
        }

        public HostDto InsertHost(HostDto host)
        {
            if(_hostsDao.InsertHost(host).IsFaulted)
            {
                throw new InternalErrorException($"Error while inserting host : {host.Name}");
            }

            return host;
        }

        public void UpdateHost(int id, HostDto host)
        {
            if(_hostsDao.UpdateHost(id, host).IsFaulted)
            {
                throw new InternalErrorException($"Error while updating host : {host.Name}");
            }
        }
        public void DeleteHost(int id)
        {
            if(_hostsDao.DeleteHost(id).IsFaulted)
            {
                throw new InternalErrorException($"Error while deleting host with id {id}");
            }
        }
    }
}
