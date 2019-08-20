using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.Models
{
    public enum OfferActionType
    {
        Non= 0, // It's means nothing
        ADD = 1,  // A new item to the pricelist
        CHG = 2,  // Changes to the pricelist e.g. SKU description changes
        DEL = 3,  // An item removed from the pricelist
        UNC = 4  //Items unchanged from the previous month's pricelist
    }

    public class OfferActionTypes
    {
        //public Models.OfferActionType GetType(string t)
        //{
        //    switch (t)
        //    {
        //        case OfferActionType.ADD:
        //            return OfferActionType.ADD;

        //    }
        //}

        public static OfferActionType ParseEnum<OfferActionType>(string value)
        {
            return (OfferActionType)Enum.Parse(typeof(OfferActionType), value, true);
        }
    }
}