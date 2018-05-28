using Microsoft.SharePoint.Client;
using System;

namespace Roro.Activities.SharePoint
{
    public class CreateList : ProcessNodeActivity
    {

        public Input<Text> SiteUrl { get; set; }

        public Input<Text> ListTitle { get; set; }

        public override void Execute(ActivityContext context)
        {

            ClientContext spContext = new ClientContext(context.Get(this.SiteUrl));
            Web spWeb = spContext.Web;

            ListCreationInformation lci = new ListCreationInformation();
            lci.Title = context.Get(this.ListTitle);
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
