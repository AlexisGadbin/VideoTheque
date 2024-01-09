using Newtonsoft.Json;
using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.Hosts;
using VideoTheque.ViewModels;

namespace VideoTheque.Businesses.Emprunts
{
    public class EmpruntsBusiness : IEmpruntsBusiness
    {
        private IHostsRepository _hostsDao;

        public EmpruntsBusiness(IHostsRepository hostsDao)
        {
            _hostsDao = hostsDao;
        }

        public async Task<List<EmpruntableViewModel>> GetEmprunts(int idHost)
        {
            HostDto host = _hostsDao.GetHost(idHost).Result;

            if (host == null)
            {
                throw new NotFoundException("Host not found");
            }
            HttpClient http = new HttpClient();

            HttpResponseMessage response = await http.GetAsync(host.Url+"/films/empruntables");

            if (response.IsSuccessStatusCode)
            {
                String json = await response.Content.ReadAsStringAsync();
                List<EmpruntableViewModel> empruntables = JsonConvert.DeserializeObject<List<EmpruntableViewModel>>(json);

                return empruntables;
            }




            throw new InternalErrorException("Error while getting empruntables");
        }

        public void Emprunter(int idHost, int idFilm)
        {
            throw new NotImplementedException();
        }
    }
}
