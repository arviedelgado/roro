using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;

namespace Roro.Activities.SharePoint
{
    class CreateListItem : ProcessNodeActivity
    {
        public Input<Text> SiteUrl { get; set; }
        public Input<Text> ListTitle { get; set; }
        public Input<Text> ItemTitle { get; set; }

        public override void Execute(ActivityContext context)
        {
            //TODO: Handle fields other than the Title field.
            ClientContext clientContext = new ClientContext(context.Get(this.SiteUrl));

            List list = clientContext.Web.Lists.GetByTitle(context.Get(this.ListTitle));
            ListItemCreationInformation lci = new ListItemCreationInformation();

            ListItem newItem = list.AddItem(lci);
            newItem["Title"] = context.Get(this.ItemTitle);
            newItem.Update();

            clientContext.ExecuteQuery();
        }
    }
}
