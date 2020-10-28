using CSharp.Choices;
using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Question.Domain.CreateQuestionWorkflow
{
    [AsChoice]
    public static partial class Vote
    {
        public interface IVote { }
        public class UnverifiedNumberVotes : IVote
        {
            public int Votes { get; private set; }
            private UnverifiedNumberVotes(int votes)
            {
                Votes = votes;
            }

            public static Result<UnverifiedNumberVotes> Create(int votes)
            {
                if (IsVotesValid(votes))
                {
                    return new UnverifiedNumberVotes(votes);
                }
                else
                {
                    return new Result<UnverifiedNumberVotes>(new InvalidVoteException(votes));
                }
            }
            private static bool IsVotesValid(int votes)
            {
                if (votes != 0)
                {
                    return true;
                }
                return false;
            }
        }

        public class VerifiedVotes : IVote
        {
            public int Votes { get; private set; }
            internal VerifiedVotes(int votes)
            {
                Votes = votes;
            }
        }

    }
}
