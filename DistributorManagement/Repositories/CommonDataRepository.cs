using DistributorManagement.Models;
using DistributorManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DistributorManagement.Repositories
{
    public class CommonDataRepository
    {
        private DistributorManagementContext db = new DistributorManagementContext();

        public static SelectList getCustomSelectList(List<CustomSelectItemViewModel> Items)
        {
            if (LoginInfo.CodeBasedSetup)
            {
                return new SelectList(Items.OrderBy(r => r.Code).ThenBy(r => r.Name).Select(r => new CustomSelectItemViewModel()
                {
                    ID = r.ID,
                    Name = r.Name + ( !String.IsNullOrEmpty(r.Code) ? " (" + r.Code + ")" : "")
                }), "ID", "Name");
            }
            else
            {
                return new SelectList(Items.OrderBy(r => r.Name).Select(r => new CustomSelectItemViewModel()
                {
                    ID = r.ID,
                    Name = r.Name
                }), "ID", "Name");
            }
        }
    }
}