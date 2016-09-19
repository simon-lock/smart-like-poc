using SmartLike_POC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLike_POC.Service
{
    public interface ISmartLikeFilterService
    {
        IEnumerable<ColourfulShape> PerformSmartLikeFilter(List<ColourfulShape> allEligibleCandidates);
    }

    public class SmartLikeFilterService : ISmartLikeFilterService
    {
        private readonly ISmartLikeCandidateScorer _candidateScorer;

        public SmartLikeFilterService(ISmartLikeCandidateScorer candiateScorer)
        {
            _candidateScorer = candiateScorer;
        }

        public IEnumerable<ColourfulShape> PerformSmartLikeFilter(List<ColourfulShape> allEligibleCandidates)
        {
            // Return a max of 20% unscored candidates.
            var count = allEligibleCandidates.Count;
            var unscoredMaxCount = Math.Ceiling(count * 0.2);

            // Aim for at least 40% score.
            const double minimumScore = 0.4;

            var unscoredCandidates = new List<ColourfulShape>();
            var smartLikedCandidates = new List<ColourfulShape>();

            foreach (ColourfulShape candidate in allEligibleCandidates)
            {
                if (_candidateScorer.HasSmartLikeScore(candidate) == false)
                {
                    if (unscoredCandidates.Count < unscoredMaxCount)
                    {
                        unscoredCandidates.Add(candidate);
                    }
                }
                else
                {
                    var score = _candidateScorer.ScoreCandidate(candidate);
                    if (score >= minimumScore)
                    {
                        smartLikedCandidates.Add(candidate);
                    }
                }
            }

            var unscoredAndSmartLikedCandidates = unscoredCandidates.Concat(smartLikedCandidates);

            var shuffled = Shuffle(unscoredAndSmartLikedCandidates);

            return shuffled;
        }

        private IEnumerable<ColourfulShape> Shuffle(IEnumerable<ColourfulShape> source)
        {
            // TODO: Shuffle logic.
            return source;
        }

    }
}
