namespace RFID
{
    public class TestManager
    {
        private Form1 _instance;
        private AuthorizationData[] _authorizationDatas = {
            new AuthorizationData {code = "0585DF14", location = Location.CTECKA},
            new AuthorizationData {code = "57192BFA", location = Location.CTECKA},
            new AuthorizationData {code = "A56B691A", location = Location.CTECKA},
            new AuthorizationData {code = "B7924124", location = Location.CTECKA},
            new AuthorizationData {code = "B7924124", location = Location.CTECKA_CISELNIKU},
            new AuthorizationData {code = "2BFF628A", location = Location.CTECKA},
        };

        public TestManager(Form1 instance)
        {
            _instance = instance;
        }

        public AuthorizationData[] GetTestData() => _authorizationDatas; // Get test authorization datas

        public void TestAuthorization(AuthorizationData authData) => _instance.GetAuthManager().Authorize(authData); // Test authorization

    }
}