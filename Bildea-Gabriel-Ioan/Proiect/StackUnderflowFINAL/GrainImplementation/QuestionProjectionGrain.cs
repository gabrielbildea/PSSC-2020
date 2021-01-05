using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrainInterfaces;
using Microsoft.EntityFrameworkCore;
using Orleans;
using Orleans.Streams;
using StackUnderflow.EF.Models;

namespace GrainImplementation
{
    public class QuestionProjectionGrain: Orleans.Grain, IQuestionProjectionGrain
    {

        private readonly StackUnderflowContext _stackUnderflowContext;
        private IList<Post> _questions;
        private readonly int _tenantId = 1;

        public QuestionProjectionGrain(StackUnderflowContext stackUnderflowContext = null)
        {
            _stackUnderflowContext = stackUnderflowContext;
        }
        public async Task<IEnumerable<Post>> GetQuestionsAsync()
        {
            return _questions; //.Where(p => p.ParentPostId == null);
        }

        public async Task<IEnumerable<Post>> GetQuestionAsync(int questionId)
        {
            return _questions.Where(p => p.PostId == questionId);
        }

        public override async Task OnActivateAsync()
        {
            //todo get tenant id 
            //grain identity {organizationGuid}/{tenantId}/questionProjection
            IAsyncStream<Post> stream = this.GetStreamProvider("SMSProvider").GetStream<Post>(Guid.Empty, "questions");
            await stream.SubscribeAsync((IAsyncObserver<Post>)this);

            //_questions = await _stackUnderflowContext.Post.Include(i=>i.Vote).Where(p => p.TenantId == _tenantId).ToListAsync();
            _questions = new List<Post>() {
                new Post
                {
                    PostId = 1,
                    PostText ="My question"
                }
            };
        }

        public async Task OnNextAsync(Post item, StreamSequenceToken token = null)
        {
            //_questions.Add(item)
            //_questions = await _stackUnderflowContext.Post.Include(i => i.Vote).Where(p => p.TenantId == _tenantId).ToListAsync();
            _questions.Add(item); //= new List<Post>();
        }

        public Task OnCompletedAsync()
        {
            throw new NotImplementedException();
        }

        public Task OnErrorAsync(Exception ex)
        {
            throw new NotImplementedException();
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