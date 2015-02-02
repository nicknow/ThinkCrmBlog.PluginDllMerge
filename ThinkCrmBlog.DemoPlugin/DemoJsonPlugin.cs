using System;
using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using ThinkCrmBlog.CrmHelper;

namespace ThinkCrmBlog.DemoPlugin
{
    public class DemoJsonPlugin : IPlugin   
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            //Make your life simple and your code clean: http://nicknow.net/dynamics-crm-2011-abstracting-plugin-setup/
            var p = new PluginSetup(serviceProvider);

            var helper = new Helper(p.Service, p.Tracing);

            p.Tracing.Trace(!helper.IsTeamMember("The Best Team is Dynamics CRM", p.Context.InitiatingUserId)
                ? "Not a Member of The Best Team is Dynamics CRM"
                : "Is a Member of The Best Team is Dynamics CRM");

            p.Tracing.Trace(JsonConvert.SerializeObject(p.Context.InputParameters["Target"]));

            throw new InvalidPluginExecutionException("Checkout the Trace Log!");

        }
    }
}
