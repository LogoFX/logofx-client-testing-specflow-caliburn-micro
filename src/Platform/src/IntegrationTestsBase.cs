using Attest.Testing.Core;
using LogoFX.Client.Testing.Shared;
using Solid.Bootstrapping;
using Solid.Practices.IoC;

namespace LogoFX.Client.Testing.SpecFlow.Caliburn.Micro
{
    /// <summary>
    /// Base class for client integration tests.
    /// </summary>
    /// <typeparam name="TContainerAdapter"></typeparam>
    /// <typeparam name="TRootObject"></typeparam>
    /// <typeparam name="TBootstrapper"></typeparam>
    public abstract class IntegrationTestsBase<TContainerAdapter, TRootObject, TBootstrapper> : 
        Attest.Testing.SpecFlow.IntegrationTestsBase<TContainerAdapter, TRootObject, TBootstrapper>
        where TContainerAdapter : IIocContainer, new() 
        where TRootObject : class
        where TBootstrapper : IInitializable, IHaveContainerAdapter<TContainerAdapter>, new()
    {
        private readonly InitializationParametersResolutionStyle _resolutionStyle;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationTestsBase{TContainer, TRootViewModel, TBootstrapper}"/> class.
        /// </summary>
        /// <param name="resolutionStyle">The resolution style.</param>
        protected IntegrationTestsBase(InitializationParametersResolutionStyle resolutionStyle = InitializationParametersResolutionStyle.PerRequest)
            :base(resolutionStyle)
        {
            _resolutionStyle = resolutionStyle;
        }

        /// <summary>
        /// Provides additional opportunity to modify the test setup logic
        /// </summary>
        protected override void SetupOverride()
        {
            base.SetupOverride();
            TestHelper.Setup();
        }

        /// <summary>
        /// Called when the teardown finishes
        /// </summary>
        protected override void OnAfterTeardown()
        {
            base.OnAfterTeardown();
            if (_resolutionStyle == InitializationParametersResolutionStyle.PerRequest)
            {
                Testing.Shared.Caliburn.Micro.TestHelper.Teardown();    
            }            
        }
    }
}
