﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Kolko.Results
{
    class EnumA
    {
        public SizeType basedOn = SizeType.flexible;

        public enum SizeType
        {
            flexible = 0,
            _frame = 1
        }
    }
}
