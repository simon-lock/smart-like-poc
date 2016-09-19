using SmartLike_POC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLike_POC.Service
{
    public interface ISmartLikeStateStore
    {
        void RecordLike(ColourfulShape candidate);
        void RecordDislike(ColourfulShape candidate);
        SmartLikeRecord[] GetRecords(ColourfulShape candidate);
    }

    public class SmartLikeStateStore : ISmartLikeStateStore
    {
        private Dictionary<Colour, SmartLikeRecord> _colourRecords;
        private Dictionary<Shape, SmartLikeRecord> _shapeRecords;

        public SmartLikeStateStore()
        {
            _colourRecords = new Dictionary<Colour, SmartLikeRecord>();
            _shapeRecords = new Dictionary<Shape, SmartLikeRecord>();
        }

        public void RecordLike(ColourfulShape candidate)
        {
            SmartLikeRecord[] records = GetRecords(candidate);

            foreach (SmartLikeRecord record in records)
            {
                record.IncrementLikes();
            }
        }

        public void RecordDislike(ColourfulShape candidate)
        {
            SmartLikeRecord[] records = GetRecords(candidate);

            foreach (SmartLikeRecord record in records)
            {
                record.IncrementDislikes();
            }
        }

        public SmartLikeRecord[] GetRecords(ColourfulShape candidate)
        {
            return new SmartLikeRecord[]
            {
                GetOrAdd(candidate.Colour, _colourRecords),
                GetOrAdd(candidate.Shape, _shapeRecords),
            };
        }

        private SmartLikeRecord GetOrAdd<TKey>(TKey key, Dictionary<TKey, SmartLikeRecord> records)
        {
            SmartLikeRecord storedRecord;
            if (records.TryGetValue(key, out storedRecord) == false)
            {
                records[key] = storedRecord = new SmartLikeRecord();
            }

            return storedRecord;
        }

    }
}
