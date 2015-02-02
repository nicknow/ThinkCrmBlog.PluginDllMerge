using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using ThinkCrmBlog.CrmHelper;

namespace ThinkCrmBlog.DemoPlugin
{
    public class DemoPlugin : IPlugin   
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            //Make your life simple and your code clean: http://nicknow.net/dynamics-crm-2011-abstracting-plugin-setup/
            var p = new PluginSetup(serviceProvider);

            var helper = new Helper(p.Service, p.Tracing);

            p.Tracing.Trace(!helper.IsTeamMember("The Best Team is Dynamics CRM", p.Context.InitiatingUserId)
                ? "Not a Member of The Best Team is Dynamics CRM"
                : "Is a Member of The Best Team is Dynamics CRM");

            throw new InvalidPluginExecutionException("Checkout the Trace Log!");
        }
    }
}
