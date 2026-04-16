using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.DTOs.Library
{
    public class LibraryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public DateTime EstablishedDate { get; set; }
        public bool IsActive { get; set; }
        public int TotalBooks { get; set; }
    }
}
