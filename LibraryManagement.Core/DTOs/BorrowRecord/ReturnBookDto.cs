using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.DTOs.BorrowRecord
{
    public class ReturnBookDto
    {
        public int BorrowRecordId { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
