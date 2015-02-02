using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace ThinkCrmBlog.CrmHelper
{
    public class Helper
    {
        private readonly IOrganizationService _service;
        private readonly ITracingService _tracing;

        public Helper(IOrganizationService service, ITracingService tracing)
        {
            if (service == null) throw new ArgumentNullException("service");
            if (tracing == null) throw new ArgumentNullException("tracing");

            _service = service;
            _tracing = tracing;
        }

        public bool IsTeamMember(String teamname, Guid userId)
        {
            _tracing.Trace("CrmHelper.IsTeamMember");

            if (teamname == null) throw new ArgumentNullException("teamname");

            //No I don't normally use ColumnSet(true) in real-world code! :)
            var query = new QueryExpression("team") {ColumnSet = new ColumnSet(true)};
            query.Criteria.AddCondition(new ConditionExpression("name", ConditionOperator.Equal, teamname));
            var link = query.AddLink("teammembership", "teamid", "teamid");
            link.LinkCriteria.AddCondition(new ConditionExpression("systemuserid", ConditionOperator.Equal, userId));

            try
            {
                var results = _service.RetrieveMultiple(query);
                return results.Entities.Count > 0;
            }
            catch (Exception ex)
            {
                _tracing.Trace(
                    "Exception Verifying User {0} is a member of team {1}. Error: {2} / Inner Exception {3}. Stack Trace: {4}",
                    userId, teamname, ex.Message, ex.InnerException != null ? ex.InnerException.Message : "(None)",
                    ex.StackTrace);
                throw;
            }
        }
    }
}
