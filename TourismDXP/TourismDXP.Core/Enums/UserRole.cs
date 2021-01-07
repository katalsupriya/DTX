using System.ComponentModel;

namespace TourismDXP.Core.Enums
{
    public enum UserRole
    {
        [Description("Admin")]
        Admin = 1,
        [Description("Super Admin")]
        SuperAdmin = 2,
        [Description("Affiliate")]
        Affiliate = 3,
        [Description("Agency")]
        Agency = 4,
        [Description("Value Add Reseller")]
        Reseller = 5,
        [Description("Client")]
        Client = 6
    }
}
