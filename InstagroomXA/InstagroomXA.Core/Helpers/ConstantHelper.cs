using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagroomXA.Core.Helpers
{
    /// <summary>
    /// Helper class for storing constants
    /// </summary>
    public class ConstantHelper
    {
        public static string ImageDirectoryName { get => "instxa"; }
        public static int InitialPostsNum { get => 20; }
        public static int MinQueryLength { get => 3; }
        public static string LikeNotificationText { get => "@{0} liked your post"; }
        public static string CommentNotificationText => "@{0} commented on your post";
        public static string FollowNotificationText => "@{0} followed you";
        public static string BlankProfilePicturePath => "res:blank_profile_picture";
        public static string UserIDKey => "UserID";
        public static string FacebookProfilePictureLink => "https://graph.facebook.com/{0}/picture?type=large";

        public enum AndroidCameraRequestCodes { Camera, Gallery }
        public static int AndroidProfilePostsRecyclerViewColumnNum { get => 3; }
        public static string AndroidTabIdxBundleKey => "MasterTabIdx";
        public static string AndroidProfileToolbarState => "ProfileToolbarState";
    }
}
