using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Excel;
using System.IO;
using System.Data;
using TMRC_CSP.Models;

namespace TMRC_CSP.ViewModel.MicrosoftPriceList
{
    public class ImportPriceList
    {
        string _path;
        public ImportPriceList(string path)
        {
            _path = path;
        }

        public IExcelDataReader getExcelReader()
        {
            // ExcelDataReader works with the binary Excel file, so it needs a FileStream
            // to get started. This is how we avoid dependencies on ACE or Interop:
            FileStream stream = File.Open(_path, FileMode.Open, FileAccess.Read);
            //
            // We return the interface, so that
            IExcelDataReader reader = null;
            try
            {
                if (_path.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                if (_path.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                if (_path.EndsWith(".ods"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                return reader;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<string> getWorksheetNames()
        {
            var reader = this.getExcelReader();
            var workbook = reader.AsDataSet();
            var sheets = from DataTable sheet in workbook.Tables select sheet.TableName;
            return sheets;
        }
        public IEnumerable<DataRow> getData(string sheet, bool firstRowIsColumnNames = true)
        {
            var reader = this.getExcelReader();
            reader.IsFirstRowAsColumnNames = firstRowIsColumnNames;
            //string SheetName = getWorksheetNames().FirstOrDefault();
            var workSheet = reader.AsDataSet().Tables[sheet];
            var filteredRows = workSheet.Rows.Cast<DataRow>().Where(row => row.ItemArray.Any(field => !(field is System.DBNull)));
            // TTliteUtil.Util.WriteToEventLog("Sheet read: "+workSheet.ToString());
            var rows = from DataRow a in filteredRows select a;
            return rows;
        }

        public List<Models.ExcelPriceList> ImportPrice(DataTable dt)
        {
            List<Models.ExcelPriceList> list = new List<Models.ExcelPriceList>();
            // OfferActionType checkIsExist;
            OfferActionType actionTypes = OfferActionType.Non;
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            //Getting the default Margin for reseller
            ViewModel.DefaultMargin.DefaultMarginResellers odjDMR = new DefaultMargin.DefaultMarginResellers();
            Models.DefaultMargin defaultResellerMargin = odjDMR.GetDefaultMargin();

            //Getting the default Margin for Customer
            ViewModel.DefaultMargin.DefaultMarginUsers odjDMU = new DefaultMargin.DefaultMarginUsers();
            Models.DefaultMargin defaultUserMargin = odjDMU.GetDefaultMargin();

            foreach (DataRow item in dt.Rows)
            {
                if (item[0].ToString() != "A/C/D/U")
                {
                    if (item[0].ToString() != "")
                        actionTypes = OfferActionTypes.ParseEnum<OfferActionType>(item[0].ToString()); //item["A/C/D/U"]
                    if (item[1].ToString() != "")
                        StartDate = Convert.ToDateTime(item[1].ToString());  //item["Valid-From Date"]
                    if (item[2].ToString() != "")
                        EndDate = Convert.ToDateTime(item[2].ToString());//item["Valid To Date"]

                    AgreementType agreementTypes = AgreementTypes.ParseEnum<AgreementType>(item[5].ToString());//item["License Agreement Type"]
                    CustomerType customerTypes = CustomerTypes.ParseEnum<CustomerType>(item[8].ToString());//item["End Customer Type"]
                    LicenseType licenseTypes = LicenseTypes.ParseEnum<LicenseType>(item[7].ToString());//item["Secondary License Type"]


                    string[] unit = item[6].ToString().Split(' ');//item["Purchase Unit"]
                    PurchaseUnit purchaseUnit = PurchaseUnits.ParseEnum<PurchaseUnit>(unit[1]);
                    Models.ExcelPriceList _m = new Models.ExcelPriceList();
                    _m.ActionType = Convert.ToInt16(actionTypes);
                    _m.AgreementType = Convert.ToInt16(agreementTypes);
                    _m.CustomerType = Convert.ToInt16(customerTypes);
                    _m.LicenseType = Convert.ToInt16(licenseTypes);
                    _m.PurchaseUnitNumber = Convert.ToInt16(unit[0]);
                    _m.PurchaseUnit = Convert.ToInt16(purchaseUnit);
                    _m.MicrosoftId = item[4].ToString();//item["Offer ID"]
                    _m.StartDate = StartDate;
                    _m.EndDate = EndDate;
                    _m.Price = float.Parse(item[9].ToString());//item["List Price"]
                    _m.Name = item[3].ToString();
                    _m.CustomerPrice = item[10].ToString() != "" && item[10].ToString() != " " ? Convert.ToDouble(item[10].ToString()) : Convert.ToDouble((_m.Price * defaultUserMargin.DefaultPercentage / 100) + _m.Price);//item["ERP Price"]
                    _m.ResellerPrice = 0; //item[11].ToString() != "" && item[11].ToString() != " " ? Convert.ToDouble(item[11].ToString()) : Convert.ToDouble(((_m.Price * defaultResellerMargin.DefaultPercentage / 100) + _m.Price));//item["Reseller Price"]
                    list.Add(_m);
                }
            }
            return list;

        }
    }
}