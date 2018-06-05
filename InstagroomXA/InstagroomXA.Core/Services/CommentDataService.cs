using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstagroomXA.Core.Contracts;
using InstagroomXA.Core.Model;

using SQLite;

namespace InstagroomXA.Core.Services
{
    /// <summary>
    /// Comment data service implementation
    /// </summary>
    public class CommentDataService : ICommentDataService
    {
        private SQLiteAsyncConnection _masterConnection;


        public CommentDataService(IDBConnectionService connectionService)
        {
            _masterConnection = connectionService.GetConnection();
            _masterConnection.CreateTableAsync<Comment>();
        }

        /// <summary>
        /// Returns the comment with the specified ID (ID property is a primary key)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Comment> GetCommentByIDAsync(int commentId)
        {
            // return await _masterConnection.FindAsync<Post>(postId);
            return await _masterConnection.FindAsync<Comment>(commentId);
        }

        /// <summary>
        /// Return all (or first n) comments for the specified post
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Comment>> GetPostComments(int postId, int n = 0)
        {
            var query = _masterConnection.Table<Comment>().Where(c => (c.PostID == postId));
            if (n != 0) { query = query.Take(n); }
            return await query.ToListAsync();

            // finds all the user posts, n is irrelevant
            //var query = await _masterConnection.GetAllWithChildrenAsync<Post>(p => (p.UserID == userId));

            //return query.ToList();
        }

        /// <summary>
        /// Adds a new comment profile to the SQLite database
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<bool> AddCommentAsync(Comment newComment)
        {
            try
            {
                // await _masterConnection.InsertAsync(newPost);
                await _masterConnection.InsertAsync(newComment);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
