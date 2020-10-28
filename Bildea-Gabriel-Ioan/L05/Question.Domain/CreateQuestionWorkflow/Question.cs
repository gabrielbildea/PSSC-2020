using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CSharp.Choices;
using LanguageExt.Common;

namespace Question.Domain.CreateQuestionWorkflow
{
	[AsChoice]
	public static partial class Question
	{
		public interface IQuestion {}

		public class UnverifiedQuestion : IQuestion
		{
			public string Question { get; private set; }
			public List<string> TagList { get; private set; }

			private UnverifiedQuestion(string question, List<string> taglist)
			{
				Question = question;
				TagList = taglist;
			}

			public static Result<UnverifiedQuestion> Create(string question, List<string>taglist)
			{
				if (IsQuestionValid(question))
				{
					if (IsTagListValid(taglist))
					{
						return new UnverifiedQuestion(question, taglist);
					}
					else
					{
						return new Result<UnverifiedQuestion>(new InvalidQuestionException(taglist));
					}
				}
				else
				{
					return new Result<UnverifiedQuestion>(new InvalidQuestionException(question));
				}
			}

			private static bool IsQuestionValid(string question)
			{
				if (question.Length > 1000)
				{
					return false;
				}
				return true;
			}

			private static bool IsTagListValid(List<string> taglist)
			{
				if (taglist.Count >= 1 && taglist.Count <= 3)
				{
					return true;
				}
				return false;
			}
		}


		public class VerifiedQuestion : IQuestion
		{
			public string Question { get; private set; }
			public List<string> TagList { get; private set; }
			internal VerifiedQuestion(string question, List<string> taglist)
			{
				Question = question;
				TagList = taglist;
			}
		}

	}
}
