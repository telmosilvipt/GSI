namespace GSI.WebApi.AD
{
    public class AdUser
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Department { get; set; }

        public DateTime? AccountExpires { get; set; }

        public DateTime? LastLogon { get; set; }
    }
}
