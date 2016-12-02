﻿namespace App.Common
{
    using App.Common.Helpers;
    using App.Common.Tasks;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Routing;

    public class BaseApplication<TContext> : IApplication
    {
        public TContext Context { get; private set; }
        public ApplicationType Type { get; private set; }
        public BaseApplication(TContext context, ApplicationType type)
        {
            this.Context = context;
            this.Type = type;
        }

        public virtual void OnApplicationStarted()
        {
            TaskArgument<TContext> taskArg = new TaskArgument<TContext>(this.Context, this.Type);
            AssemblyHelper.ExecuteTasks<IApplicationStartedTask<TaskArgument<TContext>>, TaskArgument<TContext>>(taskArg);
            AssemblyHelper.ExecuteTasks<IApplicationReadyTask<TaskArgument<TContext>>, TaskArgument<TContext>>(taskArg, true);
        }

        public virtual void OnApplicationEnded()
        {
            TaskArgument<TContext> taskArg = new TaskArgument<TContext>(this.Context, this.Type);
            AssemblyHelper.ExecuteTasks<IApplicationEndedTask<TaskArgument<TContext>>, TaskArgument<TContext>>(taskArg);
        }

        public virtual void OnApplicationRequestStarted()
        {
            TaskArgument<TContext> taskArg = new TaskArgument<TContext>(this.Context, this.Type);
            AssemblyHelper.ExecuteTasks<IApplicationRequestStartedTask<TaskArgument<TContext>>, TaskArgument<TContext>>(taskArg);
        }

        public virtual void OnApplicationRequestEnded()
        {
            TaskArgument<TContext> taskArg = new TaskArgument<TContext>(this.Context, this.Type);
            AssemblyHelper.ExecuteTasks<IApplicationRequestEndedTask<TaskArgument<TContext>>, TaskArgument<TContext>>(taskArg);
        }

        public virtual void OnUnHandledError()
        {
            TaskArgument<TContext> taskArg = new TaskArgument<TContext>(this.Context, this.Type);
            AssemblyHelper.ExecuteTasks<IUnHandledErrorTask<TaskArgument<TContext>>, TaskArgument<TContext>>(taskArg);
        }
    }
}