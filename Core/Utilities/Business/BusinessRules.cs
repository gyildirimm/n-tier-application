using Core.Utilities.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] args)
        {
            foreach (var logic in args)
            {
                if (!logic.IsSuccess)
                {
                    return logic;
                }
            }

            return null;
        }
    }
}
