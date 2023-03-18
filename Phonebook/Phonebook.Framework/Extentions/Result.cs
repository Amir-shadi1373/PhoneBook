using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.Framework.Extentions
{
    public class Result
    {
        /// <summary>
        /// متن خطا/تایید
        /// </summary>
        public string ResultMessage { get; set; }
        /// <summary>
        /// وضعیت تسک انجام شده/نشده
        /// </summary>
        public bool ResultStatus { get; set; }
    }
}
