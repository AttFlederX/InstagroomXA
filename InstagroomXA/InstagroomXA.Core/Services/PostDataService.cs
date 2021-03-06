﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstagroomXA.Core.Contracts;
using InstagroomXA.Core.Model;

using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace InstagroomXA.Core.Services
{
    /// <summary>
    /// Post data service implementation
    /// </summary>
    public class PostDataService : IPostDataService
    {
        private SQLiteAsyncConnection _masterConnection;


        public PostDataService(IDBConnectionService connectionService)
        {
            _masterConnection = connectionService.GetConnection();
            _masterConnection.CreateTableAsync<Post>();
        }

        /// <summary>
        /// Returns the post with the specified ID (ID property is a primary key)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Post> GetPostByIDAsync(int postId)
        {
            // return await _masterConnection.FindAsync<Post>(postId);
            return await _masterConnection.FindWithChildrenAsync<Post>(postId);
        }

        /// <summary>
        /// Return all (or first n) posts by the specified user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Post>> GetUserPosts(int userId, int n = 0)
        {
            var query = _masterConnection.Table<Post>().Where(p => (p.UserID == userId));
            if (n != 0) { query = query.Take(n); }
            return await query.ToListAsync();

            // finds all the user posts, n is irrelevant
            //var query = await _masterConnection.GetAllWithChildrenAsync<Post>(p => (p.UserID == userId));

            //return query.ToList();
        }

        /// <summary>
        /// Adds a new post profile to the SQLite database
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<bool> AddPostAsync(Post newPost)
        {
            try
            {
                // await _masterConnection.InsertAsync(newPost);
                await _masterConnection.InsertWithChildrenAsync(newPost);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Updates the post profile in the database
        /// </summary>
        /// <param name="updUser"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePostAsync(Post updPost)
        {
            try
            {
                // await _masterConnection.UpdateAsync(updPost);
                await _masterConnection.UpdateWithChildrenAsync(updPost);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
