using SocialNetwork.Core.Application.Dtos.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.IServices
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest email);
    }
}
