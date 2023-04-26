using SocialNetwork.Core.Application.ViewModels.Posts;
using SocialNetwork.Core.Application.ViewModels.User;
using System.Collections.Generic;

namespace SocialNetwork.WebApp.Helpers
{
    public class ReturnPostList
    {

        public List<PostViewModel> returnPostList(List<UserViewModel> uservm)
        {
            List<PostViewModel> postvm = new();

            foreach (UserViewModel user in uservm)
            {
                foreach (PostViewModel item in user.Publicaciones)
                {            
                    postvm.Add(item);
                }
            }

            postvm = postvm.OrderByDescending(time => time.created).ToList();

            return postvm;

        }

        public List<PostViewModel> SortPostList(List<PostViewModel> posteosvm) {

            List<PostViewModel> postsvm = posteosvm.OrderByDescending(time => time.created).ToList();
            //postsvm.Reverse();

            return postsvm;
        }


    }
}
