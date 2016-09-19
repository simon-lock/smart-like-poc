using SmartLike_POC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLike_POC.Service
{
    public interface ISmartLikeCandidateProvider
    {
        IEnumerable<ColourfulShape> GetSmartCandidateSet();
    }

    public class SmartLikeCandidateProvider : ISmartLikeCandidateProvider
    {
        private readonly ICandidateService _candidateService;
        private readonly ISmartLikeFilterService _filterService;

        public SmartLikeCandidateProvider(ICandidateService candidateService, ISmartLikeFilterService filterService)
        {
            _candidateService = candidateService;
            _filterService = filterService;
        }

        public IEnumerable<ColourfulShape> GetSmartCandidateSet()
        {
            var allEligibleCandidates = _candidateService.GetAllEligibleCandidates()
                                                         .ToList();
            var smartFiltered = _filterService.PerformSmartLikeFilter(allEligibleCandidates);

            return smartFiltered;
        }

    }
}
