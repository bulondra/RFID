namespace RFID
{
    public class TestManager
    {
        private Form1 _instance;
        private AuthorizationData[] _authorizationDatas = {
            new AuthorizationData {code = "", location = Location.CTECKA},
        };

        public TestManager(Form1 instance)
        {
            _instance = instance;
        }

        public AuthorizationData[] GetTestData() => _authorizationDatas;

        public void TestAuthorization(AuthorizationData authData)
        {
            _instance.GetAuthManager().Authorize(authData);
        }

    }
}