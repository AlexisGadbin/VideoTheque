using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.AgeRatings;

namespace VideoTheque.Businesses.AgeRatings
{
    public class AgeRatingBusiness : IAgeRatingsBusiness
    {
        private readonly IAgeRatingsRepository _ageRatingsDao;

        public AgeRatingBusiness(IAgeRatingsRepository ageRatingsDao)
        {
            _ageRatingsDao = ageRatingsDao;
        }

        public Task<List<AgeRatingDto>> GetAgeRatings()
        {
            return _ageRatingsDao.GetAgeRatings();
        }

        public AgeRatingDto GetAgeRating(int id)
        {
            var ageRating = _ageRatingsDao.GetAgeRating(id).Result;

            if (ageRating is null)
            {
                throw new NotFoundException($"Age rating with id {id} not found");
            }

            return ageRating;
        }

        public AgeRatingDto InsertAgeRating(AgeRatingDto ageRating)
        {
            if(_ageRatingsDao.InsertAgeRating(ageRating).IsFaulted)
            {
                throw new InternalErrorException($"Error while inserting age rating : {ageRating.Name}");
            }

            return ageRating;
        }

        public void UpdateAgeRating(int id, AgeRatingDto ageRating)
        {
            if(_ageRatingsDao.UpdateAgeRating(id, ageRating).IsFaulted)
            {
                throw new InternalErrorException($"Error while updating age rating : {ageRating.Name}");
            }
        }

        public void DeleteAgeRating(int id)
        {
            if(_ageRatingsDao.DeleteAgeRating(id).IsFaulted)
            {
                throw new InternalErrorException($"Error while deleting age rating with id {id}");
            }
        }
    }
}
