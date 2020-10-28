using System;
using System.Collections.Generic;
using System.Text;

namespace Question.Domain.CreateQuestionWorkflow
{
    [Serializable]
    public class InvalidQuestionException : Exception
    {
        public InvalidQuestionException()
        { }

        public InvalidQuestionException(string question) : base($"Question cannot be longer than 1000 characters.")
        { }

        public InvalidQuestionException(List<string> tag) : base($" Must have min. one tag and max. three tags! Number of tags used: \"{tag.Count}\".")
        { }

    }
}
