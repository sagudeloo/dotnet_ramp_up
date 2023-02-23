First I used the service directly using Trancient and Singleton, but when I tried add the service as scoped I got the next error

    Unhandled exception. System.AggregateException: Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType: Microsoft.Extensions.Hosting.IHostedService Lifetime: Singleton ImplementationType: background_service.SecondWorker': Cannot consume scoped service 'background_service.TimeService' from singleton 'Microsoft.Extensions.Hosting.IHostedService'.)
    ---> System.InvalidOperationException: Error while validating the service descriptor 'ServiceType: Microsoft.Extensions.Hosting.IHostedService Lifetime: Singleton ImplementationType: background_service.SecondWorker': Cannot consume scoped service 'background_service.TimeService' from singleton 'Microsoft.Extensions.Hosting.IHostedService'.
    ---> System.InvalidOperationException: Cannot consume scoped service 'background_service.TimeService' from singleton 'Microsoft.Extensions.Hosting.IHostedService'.
    at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteValidator.VisitScopeCache(ServiceCallSite scopedCallSite, CallSiteValidatorState state)
    at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteVisitor`2.VisitCallSite(ServiceCallSite callSite, TArgument argument)
    at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteValidator.VisitConstructor(ConstructorCallSite constructorCallSite, CallSiteValidatorState state)
    at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteVisitor`2.VisitCallSiteMain(ServiceCallSite callSite, TArgument argument)
    at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteValidator.VisitRootCache(ServiceCallSite singletonCallSite, CallSiteValidatorState state)
    at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteVisitor`2.VisitCallSite(ServiceCallSite callSite, TArgument argument)
    at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteValidator.ValidateCallSite(ServiceCallSite callSite)
    at Microsoft.Extensions.DependencyInjection.ServiceProvider.OnCreate(ServiceCallSite callSite)
    at Microsoft.Extensions.DependencyInjection.ServiceProvider.ValidateService(ServiceDescriptor descriptor)
    --- End of inner exception stack trace ---
    at Microsoft.Extensions.DependencyInjection.ServiceProvider.ValidateService(ServiceDescriptor descriptor)
    at Microsoft.Extensions.DependencyInjection.ServiceProvider..ctor(ICollection`1 serviceDescriptors, ServiceProviderOptions options)
    --- End of inner exception stack trace ---
    at Microsoft.Extensions.DependencyInjection.ServiceProvider..ctor(ICollection`1 serviceDescriptors, ServiceProviderOptions options)
    at Microsoft.Extensions.DependencyInjection.ServiceCollectionContainerBuilderExtensions.BuildServiceProvider(IServiceCollection services, ServiceProviderOptions options)
    at Microsoft.Extensions.DependencyInjection.DefaultServiceProviderFactory.CreateServiceProvider(IServiceCollection containerBuilder)
    at Microsoft.Extensions.Hosting.Internal.ServiceFactoryAdapter`1.CreateServiceProvider(Object containerBuilder)
    at Microsoft.Extensions.Hosting.HostBuilder.InitializeServiceProvider()
    at Microsoft.Extensions.Hosting.HostBuilder.Build()
    at Program.<Main>$(String[] args) in C:\Users\stiven.agudeloo\Projects\perficient\dotnet\background_service\Program.cs:line 3