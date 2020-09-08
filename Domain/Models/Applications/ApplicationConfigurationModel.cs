namespace Domain.Models.Applications
{
    /// <summary>
    /// Model to hold the identity configuration data
    /// </summary>
    public class ApplicationConfigurationModel
    {
        /// <summary>
        /// Check to enable/disable the phone authentication
        /// </summary>
        public bool IsPhoneAuthentication { get; set; }
        /// <summary>
        /// Check to enable/disable roles in the applcation
        /// </summary>
        public bool IsRolesDefined { get; set; }
        /// <summary>
        /// Check to enable/disable the email authentication
        /// </summary>
        public bool IsEmailAuthentication { get; set; }
        /// <summary>
        /// Value for the maximum number of login (failed) attempts allowed
        /// </summary>
        public int MaxAttemptsAllowed { get; set; }
    }
}
