using Profile.Domain.CreateProfileWorkflow;
using Question.Domain.CreateQuestionWorkflow;
using System;
using System.Collections.Generic;
using System.Net;
using static Profile.Domain.CreateProfileWorkflow.CreateProfileResult;
using static Question.Domain.CreateQuestionWorkflow.CreateQuestionResult;

namespace Test.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmd = new CreateProfileCmd("Gabi", string.Empty, "Bildea", "gbildea@gmail.com");
            var result = CreateProfile(cmd);

            result.Match(
                    ProcessProfileCreated,
                    ProcessProfileNotCreated,
                    ProcessInvalidProfile
                );

            var cmd2 = new CreateQuestionCmd("Title1", "Body1", new string[] { "tag1", "tag2", "tag3" });
            var result2 = CreateQuestion(cmd2);

            result2.Match(
                   ProcessQuestionCreated,
                   ProcessQuestionNotCreated,
                  ProcessInvalidQuestion
                );

            Console.ReadLine();
        }

        private static ICreateProfileResult ProcessInvalidProfile(ProfileValidationFailed validationErrors)
        {
            Console.WriteLine("Profile validation failed: ");
            foreach (var error in validationErrors.ValidationErrors)
            {
                Console.WriteLine(error);
            }
            return validationErrors;
        }

        private static ICreateProfileResult ProcessProfileNotCreated(ProfileNotCreated profileNotCreatedResult)
        {
            Console.WriteLine($"Profile not created: {profileNotCreatedResult.Reason}");
            return profileNotCreatedResult;
        }

        private static ICreateProfileResult ProcessProfileCreated(ProfileCreated profile)
        {
            Console.WriteLine($"Profile {profile.ProfileId}");
            return profile;
        }

        public static ICreateProfileResult CreateProfile(CreateProfileCmd createProfileCommand)
        {
            if (string.IsNullOrWhiteSpace(createProfileCommand.EmailAddress))
            {
                var errors = new List<string>() { "Invalid email address" };
                return new ProfileValidationFailed(errors);
            }

            if(new Random().Next(5) > 1)
            {
                return new ProfileNotCreated("Email could not be verified");
            }

            var profileId = Guid.NewGuid();
            var result = new ProfileCreated(profileId, createProfileCommand.EmailAddress);

            //execute logic
            return result;
        }


        private static ICreateQuestionResult ProcessInvalidQuestion(QuestionValidationFailed validationErrors)
        {
            Console.WriteLine("Question validation failed: ");
            foreach (var error in validationErrors.ValidationErrors)
            {
                Console.WriteLine(error);
            }
            return validationErrors;
        }
        private static ICreateQuestionResult ProcessQuestionNotCreated(QuestionNotCreated questionNotCreatedResult)
        {
            Console.WriteLine($"Question not created: {questionNotCreatedResult.Reason}");
            return questionNotCreatedResult;
        }
        private static ICreateQuestionResult ProcessQuestionCreated(QuestionCreated question)
        {
            Console.WriteLine($"Question {question.QuestionId}");
            return question;
        }
        public static ICreateQuestionResult CreateQuestion(CreateQuestionCmd createQuestionCommand)
        {
            bool isMLApproved = true;
            if (string.IsNullOrWhiteSpace(createQuestionCommand.Title))
            {
                var errors = new List<string>() { "Invalid title" };
                return new QuestionValidationFailed(errors);
            }
            else if(string.IsNullOrWhiteSpace(createQuestionCommand.Body))
            {
                var errors = new List<string>() { "Invalid body" };
                return new QuestionValidationFailed(errors);
            }
            if (new Random().Next(5) > 1)
            {
                isMLApproved = false;
                return new QuestionNotCreated("Question is not approved");
            }
            var questionId = Guid.NewGuid();
            var results = new QuestionCreated(questionId, createQuestionCommand.Title ,"gbildea@gmail.com" , isMLApproved);
            return results;
        }
    }
}
