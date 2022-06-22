namespace Bkav.eGovCloud.Api.Dto
{
    public class UserDto
    {
        public string Username { get; set; }

        public string FullName { get; set; }

        public string Position { get; set; }
    }

    public class UserBwssDto
    {
        public string UserId { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentExt { get; set; }

        public int PositionId { get; set; }
    }
}