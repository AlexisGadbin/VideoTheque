using VideoTheque.DTOs;
using VideoTheque.ViewModels;

namespace VideoTheque.Businesses.Emprunts
{
    public interface IEmpruntsBusiness
    {
        Task<List<EmpruntableViewModel>> GetEmprunts(int idHost);

        void Emprunter(int idHost, int idFilm);
    }
}
