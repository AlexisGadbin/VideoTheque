using Microsoft.EntityFrameworkCore;
using VideoTheque.Context;
using VideoTheque.DTOs;

namespace VideoTheque.Repositories.Personnes
{
    public class PersonnesRepository : IPersonnesRepository
    {
        private readonly VideothequeDb _db;

        public PersonnesRepository(VideothequeDb db)
        {
            _db = db;
        }

        public Task<List<PersonneDto>> GetPersonnes()
        {
            return _db.Personnes.ToListAsync();
        }

        public ValueTask<PersonneDto?> GetPersonne(int id)
        {
           return _db.Personnes.FindAsync(id);
        }

        public Task InsertPersonne(PersonneDto personne)
        {
            _db.Personnes.Add(personne);
            return _db.SaveChangesAsync();
        }

        public Task UpdatePersonne(int id, PersonneDto personne)
        {
            var personneToUpdate = _db.Personnes.FindAsync(id).Result;

            if (personneToUpdate is null)
            {
                throw new KeyNotFoundException($"Person with id {id} not found");
            }

            personneToUpdate.LastName = personne.LastName;
            personneToUpdate.FirstName = personne.FirstName;
            personneToUpdate.BirthDay = personne.BirthDay;
            return _db.SaveChangesAsync();
        }

        public Task DeletePersonne(int id)
        {
            var personneToDelete = _db.Personnes.FindAsync(id).Result;

            if (personneToDelete is null)
            {
                throw new KeyNotFoundException($"Person with id {id} not found");
            }

            _db.Personnes.Remove(personneToDelete);
            return _db.SaveChangesAsync();
        }
    }
}
