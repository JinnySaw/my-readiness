using System;

namespace myreadiness.API.Helpers
{
    public class UserParams
    {
        private const int MaxPageSize = 10;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 5;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public Guid EmpID { get; set; }
        public Guid RoleId { get; set; }
    }
}