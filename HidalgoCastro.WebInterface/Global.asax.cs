using FluentValidation.WebApi;
using HidalgoCastro.WebInterface.Hubs;
using HidalgoCastro.WebInterface.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace HidalgoCastro.WebInterface
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Configure FluentValidation model validator provider
            FluentValidationModelValidatorProvider.Configure(GlobalConfiguration.Configuration);

            var auctionsTimer = new System.Timers.Timer();
            auctionsTimer.Elapsed += auctionsTimer_Elapsed;
            auctionsTimer.Interval = 1000;
            auctionsTimer.Enabled = true;
        }

        void auctionsTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<AuctionsHub>();

            var result = new ListAuctions
            {
                Data = AuctionsHub.GetAuctions(),
                ServerDateTime = DateTime.Now
            };

            context.Clients.All.addMessage(result);
        }

    }
}
