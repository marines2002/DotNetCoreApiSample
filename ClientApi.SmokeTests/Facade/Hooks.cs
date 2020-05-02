using BoDi;
using TechTalk.SpecFlow;

namespace ClientApi.SmokeTests.Facade
{
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;

        public Hooks(IObjectContainer objectContainer)
        {
            this._objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void InitializeObjects()
        {
            var apiFacade = new EcsdApiFacade();

            _objectContainer.RegisterInstanceAs(apiFacade);
        }
    }
}
