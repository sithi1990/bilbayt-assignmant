using Assignment.Application.Features.Accounts.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Web.Models
{
    public class UserInfoResponse : ResponseMetadata
    {
        public UserInfoResult UserInfo { get; set; }

    }
}
