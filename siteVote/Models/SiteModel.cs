using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace siteVote.Models
{
    public class VoteSheet
    {
        public int Id { get; set; }
        public List<Vote> Votes { get; set; }

        
    }

    public class Vote
    {
        public int Id { get; set; }
        virtual public VoteSheet VoteSheet { get; set; }
        public int Score { get; set; }
    }
}