using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Question.Domain.CreateQuestionWorkflow.Question;

namespace Question.Domain.CreateQuestionWorkflow
{
    public class VoteService
    {
        public Task Vote(VerifiedQuestion question)
        {
            return Task.CompletedTask;
        }
    }
}
