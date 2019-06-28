using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdl.Mail
{
    public interface ISendMail
    {
        void CreateHost(ConfigHost host);
        void CreateMail(ConfigMail mail);
        void CreateMultiMail(ConfigMail mail);
        void SendMail();
    }
}
