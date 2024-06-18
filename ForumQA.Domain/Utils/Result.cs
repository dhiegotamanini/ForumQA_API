using ForumQA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumQA.Domain.Utils
{
    public static  class Result
    {
        public static ForumQAResult<T> GetReturnFormated<T>(T rs, string message, int statusCode)
        {
            return new ForumQAResult<T>()
            {
                Data = rs,
                Message = message,
                StatusCode = statusCode
            };
        }

        public static PageResults<T> FormatPageResult<T>(int pageIndex, int itemsPerPage, List<T> rs)
        {
            return new PageResults<T>()
            {
                TotalItems = rs?.Count ?? 0,
                Items = rs?.Skip((pageIndex - 1) * itemsPerPage).Take(itemsPerPage).ToList(),
                PageIndex = pageIndex,
                TotalPages = rs?.Count > 0 ? (int)Math.Ceiling(rs.Count / (double)itemsPerPage) : 0,
                StatusCode = 200
            };
        }

    }
}
