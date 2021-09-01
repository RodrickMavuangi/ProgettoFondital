using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fondital.Shared.Enums
{
    public enum DurataValiditaConfigurazione
    {
        [Description("1 Mese")]
        UnMese = 1,
        [Description("2 Mesi")]
        DueMesi,
        [Description("3 Mesi")]
        TreMesi,
        [Description("4 Mesi")]
        QuattroMesi,
        [Description("5 Mesi")]
        CinqueMesi,
        [Description("6 Mesi")]
        SeiMesi
    }
}
