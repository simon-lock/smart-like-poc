using SmartLike_POC.Model;
using SmartLike_POC.MVVM;
using SmartLike_POC.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartLike_POC.SmartLike
{
    public interface ISmartLikeViewModel
    {
        void Initialise();
    }

    public class SmartLikeViewModel : NotifyPropertyChangedViewModel, ISmartLikeViewModel
    {
        private readonly ISmartLikeCandidateProvider _candidateProvider;
        private readonly ISmartLikeStateStore _stateStore;
        private readonly Queue<ColourfulShape> _candidateQueue;

        public SmartLikeViewModel(ISmartLikeCandidateProvider candidateProvider, ISmartLikeStateStore stateStore)
        {
            _candidateProvider = candidateProvider;
            _stateStore = stateStore;
            _candidateQueue = new Queue<ColourfulShape>();
        }

        public void Initialise()
        {
            FetchMoreSmartCandidates();
        }

        private ICommand _like;
        public ICommand LikeCmd { get { return _like ?? (_like = new RelayCommand(DoLikeActiveCandidate, CanProcessActiveCanddiate)); } }

        private ICommand _dislike;
        public ICommand DislikeCmd { get { return _dislike ?? (_dislike = new RelayCommand(DoDislikeActiveCandidate, CanProcessActiveCanddiate)); } }

        private ColourfulShape _activeCandidate;
        public ColourfulShape ActiveCandidate
        {
            get { return _activeCandidate; }
            set { OnChange(ref _activeCandidate, value); }
        }

        private void FetchMoreSmartCandidates()
        {
            var smartCandidates = _candidateProvider.GetSmartCandidateSet();
            foreach (var candidate in smartCandidates)
            {
                _candidateQueue.Enqueue(candidate);
            }

            TryShowNextCandidate();
        }

        private bool CanProcessActiveCanddiate()
        {
            return ActiveCandidate != null;
        }

        private void DoLikeActiveCandidate()
        {
            ProcessActiveCandidate((candidate, stateStore) => stateStore.RecordLike(candidate));
        }

        private void DoDislikeActiveCandidate()
        {
            ProcessActiveCandidate((candidate, stateStore) => stateStore.RecordDislike(candidate));
        }

        private void ProcessActiveCandidate(Action<ColourfulShape, ISmartLikeStateStore> record)
        {
            record(ActiveCandidate, _stateStore);
                        
            if (TryShowNextCandidate()) return;

            FetchMoreSmartCandidates();
        }

        private bool TryShowNextCandidate()
        {
            if (_candidateQueue.Count == 0)
            {
                ActiveCandidate = null;
                return false;
            }

            ActiveCandidate = _candidateQueue.Dequeue();
            return true;
        }

    }
}
