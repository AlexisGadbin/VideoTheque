using Microsoft.EntityFrameworkCore;
using VideoTheque.Context;
using VideoTheque.DTOs;

namespace VideoTheque.Repositories.AgeRatings
{
    public class AgeRatingRepository : IAgeRatingsRepository
    {

        private readonly VideothequeDb _db;

        public AgeRatingRepository(VideothequeDb db)
        {
            _db = db;
        }

        public Task<List<AgeRatingDto>> GetAgeRatings()
        {
            return _db.AgeRatings.ToListAsync();
        }

        public ValueTask<AgeRatingDto?> GetAgeRating(int id)
        {
            return _db.AgeRatings.FindAsync(id);
        }

        public Task<AgeRatingDto?> GetAgeRating(string name)
        {
            return _db.AgeRatings.FirstOrDefaultAsync(ageRating => ageRating.Name == name);
        }

        public Task InsertAgeRating(AgeRatingDto ageRating)
        {
            _db.AgeRatings.Add(ageRating);
            return _db.SaveChangesAsync();
        }

        public Task UpdateAgeRating(int id, AgeRatingDto ageRating)
        {
            var ageRatingToUpdate = _db.AgeRatings.FindAsync(id).Result;

            if (ageRatingToUpdate is null)
            {
                throw new KeyNotFoundException($"Age rating with id {id} not found");
            }

            ageRatingToUpdate.Name = ageRating.Name;
            ageRatingToUpdate.Abreviation = ageRating.Abreviation;
            return _db.SaveChangesAsync();
        }

        public Task DeleteAgeRating(int id)
        {
            var ageRatingToDelete = _db.AgeRatings.FindAsync(id).Result;

            if (ageRatingToDelete is null)
            {
                throw new KeyNotFoundException($"Age rating with id {id} not found");
            }

            _db.AgeRatings.Remove(ageRatingToDelete);
            return _db.SaveChangesAsync();
        }
    }
}
