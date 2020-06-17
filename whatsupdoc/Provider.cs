using System;
namespace whatsupdoc
{
    public class Provider
    {
        public string ProviderName { get; set; }
        public string ProviderSpecialty { get; set; }
        public string ProviderOrganization { get; set; }

        public override string ToString()
        {
            return ProviderSpecialty;
        }
    }
}
