using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.Personnes;

namespace VideoTheque.Businesses.Personnes
{
    public class PersonnesBusiness : IPersonnesBusiness
    {
        private readonly IPersonnesRepository _personnesDao;

        public PersonnesBusiness(IPersonnesRepository personnesDao)
        {
            _personnesDao = personnesDao;
        }
        public Task<List<PersonneDto>> GetPersonnes()
        {
            return _personnesDao.GetPersonnes();
        }

        public PersonneDto GetPersonne(int id)
        {
            var ageRating = _personnesDao.GetPersonne(id).Result;

            if(ageRating is null)
            {
                throw new NotFoundException($"Personne with id {id} not found");
            }

            return ageRating;
        }

        public PersonneDto GetPersonne(string firstName, string lastName)
        {
            var ageRating = _personnesDao.GetPersonne(firstName, lastName).Result;

            if(ageRating is null)
            {
                throw new NotFoundException($"Personne with name {firstName} {lastName} not found");
            }

            return ageRating;
        }

        public PersonneDto InsertPersonne(PersonneDto personne)
        {
            if(_personnesDao.InsertPersonne(personne).IsFaulted)
            {
                throw new InternalErrorException($"Error while inserting personne : {personne.FirstName} {personne.LastName}");
            }

            return personne;
        }

        public void UpdatePersonne(int id, PersonneDto personne)
        {
            if(_personnesDao.UpdatePersonne(id, personne).IsFaulted)
            {
                throw new InternalErrorException($"Error while updating personne : {personne.FirstName} {personne.LastName}");
            }
        }

        public void DeletePersonne(int id)
        {
            if(_personnesDao.DeletePersonne(id).IsFaulted)
            {
                throw new InternalErrorException($"Error while deleting personne with id {id}");
            }
        }
    }
}
