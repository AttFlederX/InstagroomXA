using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstagroomXA.Core.Model;

namespace InstagroomXA.Core.Contracts
{
    /// <summary>
    /// Interface for post data services' classes
    /// </summary>
    public interface IPostDataService
    {
        Task<Post> GetPostByIDAsync(int postId);
        Task<IEnumerable<Post>> GetUserPosts(int userId, int n);

        Task<bool> AddPostAsync(Post newPost);
        Task<bool> UpdatePostAsync(Post updPost);
    }
}
