using Component.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Components.Interfaces
{
    public interface IGroupComponent
    {
        bool Add(GroupModel group, int userId);
        bool Edit(GroupModel group);
        bool AddMember(int groupId, int userId);
        bool RemoveMember(int groupId, int userId);
        
        /// <param name="userId">
        /// returns groups for this user, leave empty if want get all groups
        /// </param>
        IEnumerable<GroupModel> Get(int? userId = null);
        GroupModel GetDetails(int id);


    }
}
