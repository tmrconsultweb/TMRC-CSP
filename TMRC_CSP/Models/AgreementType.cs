using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    public enum AgreementType
    {
        Corporate = 1,
        Charity = 2,
        Academic = 3,
        Government = 4
    }

    public static class AgreementTypes
    {
        public static AgreementType ParseEnum<AgreementType>(string value)
        {
            return (AgreementType)Enum.Parse(typeof(AgreementType), value, true);
        }

        public static string ParseEnumToString<AgreementType>(int value)
        {
            return Enum.GetName(typeof(TMRC_CSP.Models.AgreementType), value);
        }
    }
}