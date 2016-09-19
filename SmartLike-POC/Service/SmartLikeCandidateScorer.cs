using SmartLike_POC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLike_POC.Service
{
    public interface ISmartLikeCandidateScorer
    {
        bool HasSmartLikeScore(ColourfulShape candidate);
        double ScoreCandidate(ColourfulShape candidate);
    }

    public class SmartLikeCandidateScorer : ISmartLikeCandidateScorer
    {
        private readonly ISmartLikeStateStore _stateStore;

        public SmartLikeCandidateScorer(ISmartLikeStateStore stateStore)
        {
            _stateStore = stateStore;
        }

        public bool HasSmartLikeScore(ColourfulShape candidate)
        {
            var records = _stateStore.GetRecords(candidate);

            return records.Any(HasSmartLikeScore);            
        }

        public double ScoreCandidate(ColourfulShape candidate)
        {
            var records = _stateStore.GetRecords(candidate);

            var candidateScore = CalculateSmartLikeScore(records);
            return candidateScore;
        }

        private bool HasSmartLikeScore(SmartLikeRecord candidateRecord)
        {
            return candidateRecord.Likes + candidateRecord.Dislikes != 0;
        }

        private double CalculateSmartLikeScore(IEnumerable<SmartLikeRecord> candidateRecords)
        {
            var scoreVector = Math.Sqrt(
                candidateRecords.Where(HasSmartLikeScore)
                                .Sum(r => Square(CalculateSmartLikeScore(r))));

            return scoreVector;
        }

        private static double CalculateSmartLikeScore(SmartLikeRecord record)
        {
            return record.Likes / (record.Likes + record.Dislikes);
        }

        private static double Square(double value)
        {
            return value * value;
        }

    }
}
