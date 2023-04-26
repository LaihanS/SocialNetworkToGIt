using SocialNetwork.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Domain.Entities
{
    public class User_FriendTable: AuditableBaseEntity
    {
        public int idUser { get; set; }
        public int idAmigo { get; set; } 

        public Usuario Usuario { get; set; }

    }
}
