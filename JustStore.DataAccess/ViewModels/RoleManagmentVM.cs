using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Entity;

namespace DataAccess.Models.ViewModels
{
    public class RoleManagementVM
    {
        public ApplicationUser ApplicationUser { get; set; } = default!;
        public IEnumerable<SelectListItem> RoleList { get; set; } = default!;
        public IEnumerable<SelectListItem> CompanyList { get; set; } = default!;
    }
}
