using SmartLike_POC.Service;
using SmartLike_POC.SmartLike;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLike_POC
{
    class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            SetUp();
        }

        public void SetUp()
        {
            var stateStore = new SmartLikeStateStore();

            var smartLike = new SmartLikeViewModel(
                new SmartLikeCandidateProvider(
                    new CandidateService(),
                    new SmartLikeFilterService(
                        new SmartLikeCandidateScorer(stateStore))),
                stateStore);

            smartLike.Initialise();

            SmartLike = smartLike;
        }

        public ISmartLikeViewModel SmartLike { get; private set; }

    }
}
