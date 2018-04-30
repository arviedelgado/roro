using System;
using Microsoft.SharePoint.Client;

namespace Roro.Activities.SharePoint
{
    public class CreateList : ProcessNodeActivity
    {

        public Input<Text> SiteUrl { get; set; }
        public Input<Text> ListTitle { get; set; }

        public override void Execute(ActivityContext context)
        {

            ClientContext spContext = new ClientContext(this.SiteUrl.ToString());
            Web spWeb = spContext.Web;

            ListCreationInformation lci = new ListCreationInformation();
            lci.Title = this.ListTitle.ToString();
            lci.TemplateType = (int)ListTemplateType.DocumentLibrary;

            List newList = spWeb.Lists.Add(lci);

            try
            {
                spContext.ExecuteQuery();
            }
            catch
            {
                throw new NotImplementedException();
            }

        }
    }
}
