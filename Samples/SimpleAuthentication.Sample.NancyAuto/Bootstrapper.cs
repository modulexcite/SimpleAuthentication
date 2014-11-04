﻿using System;
using System.Collections.Generic;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Session;
using Nancy.TinyIoc;
using SimpleAuthentication.Core.Providers;
using SimpleAuthentication.ExtraProviders;

namespace SimpleAuthentication.Sample.NancyAuto
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            // Manually adding our own Types (instead of scanning for them or they exist in a weird location
            // where the scanner can't find them).
            var additionalProviderTypes = new List<Type>
            {
                typeof (GitHubProvider),
                typeof (InstagramProvider)
            };
            var providerScanner = new ProviderScanner(additionalProviderTypes);
            container.Register<IProviderScanner, ProviderScanner>(providerScanner);

            //var authenticationProviderFactory = new AuthenticationProviderFactory(providerScanner);

            //// TODO: make APF a singleton.
            //authenticationProviderFactory.AddProvider(gitHubProvider);
            //authenticationProviderFactory.AddProvider(instagramProvider);

            CookieBasedSessions.Enable(pipelines);
        }
    }
}