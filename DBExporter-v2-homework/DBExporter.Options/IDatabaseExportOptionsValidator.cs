﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExporter.Options
{
    public interface IDatabaseExportOptionsValidator
    {
        void Validate(DatabaseExportOptions options);
    }
}
