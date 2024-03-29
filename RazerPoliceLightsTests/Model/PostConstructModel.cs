using RazerPoliceLightsBase;

namespace RazerPoliceLightsTests.Model
{
    public class PostConstructModel : IPostConstructModel
    {
        public bool IsInitialized { get; private set; }
        
        [IoC.PostConstruct]
        private void Init()
        {
            IsInitialized = true;
        }
    }
}