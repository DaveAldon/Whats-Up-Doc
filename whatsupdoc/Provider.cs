using System;
namespace whatsupdoc
{
    public class Provider
    {
        public string ProviderName { get; set; }
        public string ProviderSpecialty { get; set; }
        public string ProviderOrganization { get; set; }
        public string ProviderID { get; set; }
        public string ProviderNPI { get; set; }
        public string ProviderRole { get; set; }
        public string ProviderPhone { get; set; }
        public string ProviderEmail { get; set; }
        public string ProviderOrganizationID { get; set; }

        public string OrganizationName { get; set; }
        public string OrganizationAddress { get; set; }
        public string OrganizationCLIA { get; set; }

        public override string ToString()
        {
            return ProviderSpecialty;
        }
    }
}
