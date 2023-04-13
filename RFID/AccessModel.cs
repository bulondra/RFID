namespace RFID
{
    public class AccessModel
    {
        public int access_id { get; set; }
        public string code { get; set; }
        public string place { get; set; }
        public int available_from { get; set; }
        public int available_until { get; set; }
    }
}