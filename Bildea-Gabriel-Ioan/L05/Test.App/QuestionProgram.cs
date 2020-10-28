using System;
using System.Collections.Generic;
using System.Net;
using LanguageExt;
using Question.Domain.CreateQuestionWorkflow;
using static Question.Domain.CreateQuestionWorkflow.Question;

namespace Test.App
{
    class QuestionProgram
    {
        static void Main(string[] args)
        {
            List<string> tags = new List<string>() { "tag1", "tag2", "tag3" };
            var result = UnverifiedQuestion.Create("Question 1", tags);

            result.Match(
                    Succ: question =>
                    {
                        EnableVoteQuestion(question);
                        Console.WriteLine("Question can be voted.");
                        return Unit.Default;
                    },

                    Fail: ex =>
                    {
                        Console.WriteLine($"Question cannot be voted: {ex.Message}");
                        return Unit.Default;
                    }
                );

            Console.ReadLine();
        }

        private static void EnableVoteQuestion(UnverifiedQuestion question)
        {
            var verifiedQuestionResult = new VerifyQuestionService().VerifyQuestion(question);
            verifiedQuestionResult.Match(
                    EnableVoteQuestion =>
                    {
                        new VoteService().Vote(EnableVoteQuestion).Wait();
                        return Unit.Default;
                    },
                    ex =>
                    {
                        Console.WriteLine("Question cannot be voted.");
                        return Unit.Default;
                    }
                );
        }
    }
}
