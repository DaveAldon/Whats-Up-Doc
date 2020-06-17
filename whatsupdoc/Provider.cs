namespace whatsupdoc
{
    // Definition of a provider object, with all of the accessible fields that are bound to the context of pages
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
        public string OrganizationCity { get; set; }
        public string OrganizationState { get; set; }
        public string OrganizationNPI { get; set; }

        public override string ToString()
        {
            return ProviderSpecialty;
        }
    }
}
