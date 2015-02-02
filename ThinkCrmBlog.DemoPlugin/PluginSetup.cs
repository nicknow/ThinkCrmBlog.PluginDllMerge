using System;
using Microsoft.Xrm.Sdk;

namespace ThinkCrmBlog.DemoPlugin
{
    public class PluginSetup
    {
        private IPluginExecutionContext _context;
        private readonly IServiceProvider _serviceProvider;
        private IOrganizationServiceFactory _serviceFactory;
        private IOrganizationService _service;
        private ITracingService _tracing;

        public PluginSetup(IServiceProvider ServiceProvider)
        {
            if (ServiceProvider == null) throw new Exception("PluginSetup requires a valid IServiceProvider.");
            _serviceProvider = ServiceProvider;
        }

        public IServiceProvider ServiceProvider
        {
            get { return _serviceProvider; }
        }

        public IPluginExecutionContext Context
        {
            get
            {
                if (_context != null)
                {
                    return _context;
                }
                else
                {
                    _context =
                        (Microsoft.Xrm.Sdk.IPluginExecutionContext)
                            ServiceProvider.GetService(typeof (Microsoft.Xrm.Sdk.IPluginExecutionContext));
                    return _context;
                }
            }
        }

        public IOrganizationServiceFactory ServiceFactory
        {
            get
            {
                if (_serviceFactory != null)
                {
                    return _serviceFactory;
                }
                else
                {
                    _serviceFactory =
                        (IOrganizationServiceFactory) ServiceProvider.GetService(typeof (IOrganizationServiceFactory));
                    return _serviceFactory;
                }
            }
        }

        public IOrganizationService Service
        {
            get
            {
                if (_service != null)
                {
                    return _service;
                }
                else
                {
                    _service = ServiceFactory.CreateOrganizationService(Context.UserId);
                    return _service;
                }
            }
        }

        public ITracingService Tracing
        {
            get
            {
                if (_tracing != null)
                {
                    return _tracing;
                }
                else
                {
                    _tracing = (ITracingService) ServiceProvider.GetService(typeof (ITracingService));
                    return _tracing;
                }
            }
        }
    }
}