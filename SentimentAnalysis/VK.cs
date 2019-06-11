using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace SentimentAnalysis
{
    class VK
    {
        private VkApi api = new VkApi();

        public void authorization(string login, string password)
        {
			try
			{
				api.Authorize(new ApiAuthParams
				{
					ApplicationId = 123456,
					Login = login,
					Password = password,
					Settings = Settings.All
				});
			}
			catch (Exception e)
			{
				MessageBox.Show("Error. PLease write again", e.Message);

				throw;
			}
            
        }

        public DataTable getGroups()
        {
            DataTable groupsDataTable = new DataTable();
            groupsDataTable.Columns.Add("id");
            groupsDataTable.Columns.Add("group name");
            ReadOnlyCollection<Group> groups = api.Groups.Get(new GroupsGetParams()
            {
                UserId = api.UserId,
                Extended = true
            });
            foreach (Group group in groups)
            {
                groupsDataTable.Rows.Add(group.Id, group.Name);
            }
            return groupsDataTable;
        }

        public ReadOnlyCollection<VkNet.Model.Attachments.Post> getGroupPosts(long groupId, ulong countPosts)
        {
            var posts = api.Wall.Get(new WallGetParams()
            {
                OwnerId = -groupId,
                Count = countPosts
            }).WallPosts;
            return posts;
        }

        public DataTable getGroupComents(long groupId, long postId)
        {
            DataTable commentsDataTable = new DataTable();
            commentsDataTable.Columns.Add("comment");
            ReadOnlyCollection<Comment> comments = api.Wall.GetComments(new WallGetCommentsParams
            {
                OwnerId = -groupId,
                PostId = postId
            });
            foreach (Comment comment in comments)
            {
                commentsDataTable.Rows.Add(comment.Text);
            }
            return commentsDataTable;        
        }
    }
}
