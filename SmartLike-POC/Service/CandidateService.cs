using SmartLike_POC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLike_POC.Service
{
    public interface ICandidateService
    {
        IEnumerable<ColourfulShape> GetAllEligibleCandidates();
    }

    public class CandidateService : ICandidateService
    {
        private readonly Random _random;

        public CandidateService()
        {
            _random = new Random();
        }

        public IEnumerable<ColourfulShape> GetAllEligibleCandidates()
        {
            // Load 20 random candidates.
            for (int i = 0; i < 20; i++)
            {
                yield return new ColourfulShape(GetRandomColour(), GetRandomShape());
            }
        }

        private Colour GetRandomColour()
        {
            return GetRandomEnumValue<Colour>();
        }

        private Shape GetRandomShape()
        {
            return GetRandomEnumValue<Shape>();
        }

        private TEnum GetRandomEnumValue<TEnum>()
        {
            var allPossibleValues = (TEnum[])Enum.GetValues(typeof(TEnum));
            var numberOfPossibleValues = allPossibleValues.Count();
            var index = _random.Next(0, numberOfPossibleValues);

            return allPossibleValues[index];
        }

    }
}
