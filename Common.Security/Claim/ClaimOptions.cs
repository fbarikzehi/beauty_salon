namespace Common.Security.Claim
{
    public static class ClaimOptions
    {
        public static string UserIdClaimType { get; set; } = "UserId";
        public static string UserFullNameClaimType { get; set; } = "UserFullName";
        public static string RoleTitleClaimType { get; set; } = "Role";
        public static string RoleIdClaimType { get; set; } = "RoleId";
        public static string RolePersianTitleClaimType { get; set; } = "RolePersianTitle";
    }
}
