using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrainInterfaces;
using Microsoft.EntityFrameworkCore;
using Orleans;
using Orleans.Streams;
using StackUnderflow.DatabaseModel.Models;
using StackUnderflow.EF.Models;

namespace GrainImplementation
{
    public class QuestionGrain: Orleans.Grain, IQuestionGrain
    {

        private readonly StackUnderflowContext _dbContext;
        private QuestionGrain state;

        public QuestionGrain(StackUnderflowContext dbContext)
        {
            _dbContext = dbContext;
        }

        /*public async Task OnActivateAsync(StackUnderflowContext dbContext)
        {
            var key = (int)this.GetPrimaryKeyLong();
            //Post question = new Post();
            var postList = await dbContext.Post.ToListAsync();

            var question = from post in postList.Where(r => r.PostId == key)
                           //where post.Equals(key.ToString())
                            select post.Title;

           // var expParentPostId = from parentPostId in question.ParentPostId.ToString()
           //                       where parentPostId.Equals(key.ToString())
           //                       select parentPostId;


            // subscribe to replies stream
            IStreamProvider streamProvider = GetStreamProvider("SMSProvider");
            IAsyncStream <Post> stream = streamProvider.GetStream<Post>(this.GetPrimaryKey(), "QuestionAndAnswers");
            var subscription = await stream.SubscribeAsync((IAsyncObserver<Post>)this);
            //return await Task.FromResult(question);
            // return base.OnActivateAsync();
        }*/
        //public override async Task OnActivateAsync()
        //{
            //read state from Db where postid = or parentid=
            //subscribe to reply states
            //var key = (int)this.GetPrimaryKeyLong();
            //IStreamProvider streamProvider = GetStreamProvider("SMSProvider");
            //IAsyncStream<string> stream = streamProvider.GetStream<string>(this.GetPrimaryKey(), "QuestionAndAnswers");
            //var subscription = await stream.SubscribeAsync((IAsyncObserver<string>)this);
            //base.OnActivateAsync();
        //}
        public async Task<IEnumerable<string>> SendQuestionWithAnswers(StackUnderflowContext dbContext)
        {

            var key = (int)this.GetPrimaryKeyLong();

            //IStreamProvider streamProvider = GetStreamProvider("SMSProvider");
            //IAsyncStream<Post> stream = streamProvider.GetStream<Post>(this.GetPrimaryKey(), "QuestionAndAnswers");
            //var subscription = await stream.SubscribeAsync((IAsyncObserver<Post>)this);

            //Post question = new Post();
            var postList = await dbContext.Post.ToListAsync();

            var question = from post in postList.Where(r => r.PostId == key)
                               //where post.Equals(key.ToString())
                           select post.Title.ToString();

            // var parentPostId = from post in question.ParentPostId.ToString()
            //                       where parentPostId.Equals(key.ToString())
            //                       select parentPostId;


            // subscribe to replies stream
            return await Task.FromResult(question);
            // return base.OnActivateAsync();
        }

    }
}
