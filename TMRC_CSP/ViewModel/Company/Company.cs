using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMRC_CSP.ViewModel.Company
{
    public class Company
    {
        public string Save(Models.Company _c, int ResellerId)
        {
            string images = string.Empty;
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    images = ViewModel.Common.Images.SaveImages()[0];
                    if ((images == "Invalid image" || images == "Invalid image formate" || images == "Please select an image"))
                    {
                        if (_c.LogoUrl == "")
                            return images;
                        else
                            images = _c.LogoUrl;
                    }
                }

                var db = new Context.ConnectionStringsContext();
                if (_c.Id != 0 && db.Company.Any(m => m.Id == _c.Id))  //Update
                {
                    var company = db.Company.Where(m => m.Id == _c.Id).SingleOrDefault();
                    company.Address = _c.Address;
                    company.Email = _c.Email;
                    if(images != "" && images!=null) company.LogoUrl = images;
                    company.Name = _c.Name;
                    company.PhoneNumber = _c.PhoneNumber;
                    company.POBox = _c.POBox;
                    company.ResellerId = ResellerId;
                    company.WebsiteUrl = _c.WebsiteUrl;
                }
                else  //save
                {
                    Models.Company company = new Models.Company
                    {
                        WebsiteUrl = _c.WebsiteUrl,
                        ResellerId = ResellerId,
                        POBox = _c.POBox,
                        Address = _c.Address,
                        Email = _c.Email,
                        LogoUrl = images,
                        Name = _c.Name,
                        PhoneNumber = _c.PhoneNumber,
                    };
                    db.Company.Add(company);
                }
                db.SaveChanges();
                return "Successully Company setting has been saved.";
            }
            catch (Exception ex)
            {
                return "Unknown error occur, Please try again.";
            }
        }

        public Models.Company Get(int ResellerId)
        {
            try
            {
                var db = new Context.ConnectionStringsContext();
                if (!db.Company.Any(m => m.ResellerId == ResellerId))
                    return EmptyCompany();
                return db.Company.Where(m => m.ResellerId == ResellerId).FirstOrDefault();
            }
            catch
            {
                return EmptyCompany();
            }
        }

        public Models.Company EmptyCompany()
        {
            return new Models.Company
            {
                Id = 0,
            };
        }
    }
}