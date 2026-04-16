using LibraryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Interfaces
{
    public interface IMemberRepository : IRepository<Member>
    {
        Task<Member?> GetMemberByEmailAsync(string email);
        Task<Member?> GetMemberByMembershipNumberAsync(string membershipNumber);
    }
}
