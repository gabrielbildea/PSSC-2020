using System;
using System.Collections.Generic;
using System.Text;
using StackUnderflow.DatabaseModel.Models;
using StackUnderflow.EF.Models;

namespace StackUnderflow.Domain.Core.Contexts.Questions
{
    class QuestionsReadContext
    {
        public IEnumerable<Post> Questions { get; }
        public IEnumerable<User> Users { get; }

        public QuestionsReadContext(IEnumerable<Post> questions, IEnumerable<User> users)
        {
            Questions = questions;
            Users = users;
        }
    }
}
