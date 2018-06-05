using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InstagroomXA.Core.Model;

namespace InstagroomXA.Core.Contracts
{
    /// <summary>
    /// Interface for comment data services' classes
    /// </summary>
    public interface ICommentDataService
    {
        Task<Comment> GetCommentByIDAsync(int commentId);
        Task<IEnumerable<Comment>> GetPostComments(int postId, int n);

        Task<bool> AddCommentAsync(Comment newComment);
    }
}
