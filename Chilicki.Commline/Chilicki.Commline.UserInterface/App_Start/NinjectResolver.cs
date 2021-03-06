﻿using Chilicki.Commline.Application.Correctors;
using Chilicki.Commline.Application.Managers;
using Chilicki.Commline.Application.Managers.Settings;
using Chilicki.Commline.Application.Search.Managers;
using Chilicki.Commline.Application.Search.ManualMappers;
using Chilicki.Commline.Application.Search.Validators;
using Chilicki.Commline.Application.Validators;
using Chilicki.Commline.Domain.Factories;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using Chilicki.Commline.Domain.Search.Factories.Dijkstra;
using Chilicki.Commline.Domain.Search.Services;
using Chilicki.Commline.Domain.Search.Services.Base;
using Chilicki.Commline.Domain.Search.Services.Dijkstra;
using Chilicki.Commline.Domain.Search.Services.GraphFactories;
using Chilicki.Commline.Domain.Search.Services.GraphFactories.Base;
using Chilicki.Commline.Domain.Search.Services.Path;
using Chilicki.Commline.Domain.Services.Matching;
using Chilicki.Commline.Domain.Services.Routes;
using Chilicki.Commline.Infrastructure.Databases;
using Chilicki.Commline.Infrastructure.IO;
using Chilicki.Commline.Infrastructure.Repositories;
using Chilicki.Commline.UserInterface.Controllers;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;

namespace Chilicki.Commline.UserInterface.App_Start
{
    public class NinjectResolver : System.Web.Mvc.IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<CommlineDBContext>().ToSelf().InRequestScope();

            _kernel.Bind<StopRepository>().ToSelf().InRequestScope();
            _kernel.Bind<LineRepository>().ToSelf().InRequestScope();
            _kernel.Bind<DepartureRepository>().ToSelf().InRequestScope();
            _kernel.Bind<RouteStopRepository>().ToSelf().InRequestScope();
            _kernel.Bind<MixedRepository>().ToSelf().InRequestScope();

            _kernel.Bind<StopLineTypesMatchChecker>().ToSelf();
            _kernel.Bind<NextRouteStopResolver>().ToSelf();

            _kernel.Bind<IConnectionSearchEngine>().To<DijkstraConnectionSearchEngine>();
            _kernel.Bind<IGraphFactory<StopGraph>>().To<StopGraphFactory>();
            _kernel.Bind<DijkstraNextVertexResolver>().ToSelf();
            _kernel.Bind<DijkstraEmptyFastestConnectionsFactory>().ToSelf();
            _kernel.Bind<DijkstraFastestConnectionReplacer>().ToSelf();

            _kernel.Bind<FastestPathResolver>().ToSelf();
            _kernel.Bind<FastestPathTransferService>().ToSelf();

            _kernel.Bind<StopFactory>().ToSelf();

            _kernel.Bind<SearchManager>().ToSelf();
            _kernel.Bind<StopManager>().ToSelf();
            _kernel.Bind<LineManager>().ToSelf();
            _kernel.Bind<DepartureManager>().ToSelf();
            _kernel.Bind<EditorManager>().ToSelf();

            _kernel.Bind<SearchValidator>().ToSelf();
            _kernel.Bind<LineValidator>().ToSelf();
            _kernel.Bind<DeparturesValidator>().ToSelf();
            _kernel.Bind<StopValidator>().ToSelf();

            _kernel.Bind<LineCorrector>().ToSelf();
            
            _kernel.Bind<SearchInputManualMapper>().ToSelf();

            _kernel.Bind<SettingsManager>().ToSelf();
            _kernel.Bind<SettingsSerializer>().ToSelf();
            _kernel.Bind<SettingsDeserializer>().ToSelf();

            _kernel.Bind<HomeController>().ToSelf();
            _kernel.Bind<EditorController>().ToSelf();
            _kernel.Bind<SearchController>().ToSelf();
            _kernel.Bind<SettingsController>().ToSelf();
        }
    }
}