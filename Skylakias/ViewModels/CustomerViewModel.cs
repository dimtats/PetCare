using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skylakias.Models;

namespace Skylakias.ViewModels
{
    public class CustomerViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public ApplicationUser User { get; set; }
        public virtual MembershipType MembershipType { get; set; }
    }
}