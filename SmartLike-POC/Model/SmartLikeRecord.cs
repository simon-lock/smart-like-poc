using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLike_POC.Model
{
    public class SmartLikeRecord
    {
        public int Likes { get; private set; }
        public int Dislikes { get; private set; }

        public void IncrementLikes()
        {
            Likes++;
        }

        public void IncrementDislikes()
        {
            Dislikes++;
        }

    }
}
