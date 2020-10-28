using System;
using System.Collections.Generic;
using System.Text;

namespace Question.Domain.CreateQuestionWorkflow
{
    [Serializable]
    public class InvalidVoteException : Exception
    {
        public InvalidVoteException()
        { }

        public InvalidVoteException(int votes) : base($"The score is not the same as sum of individual votes.")
        { }
    }
}
