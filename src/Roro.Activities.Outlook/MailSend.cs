using System;

namespace Roro.Activities.Outlook
{
    public class MailSend : ProcessNodeActivity
    {
        public Input<Text> From { get; set; }

        public Input<Text> To { get; set; }

        public Input<Text> Cc { get; set; }

        public Input<Text> Bcc { get; set; }

        public Input<Text> Subject { get; set; }

        public Input<Text> Body { get; set; }

        public Input<Text> Attachment { get; set; }

        public Input<TrueFalse> IsHtmlBody { get; set; }

        public Input<TrueFalse> IsDraft { get; set; }

        public override void Execute(ActivityContext context)
        {
            var from = context.Get(this.From, string.Empty);
            var to = context.Get(this.To, string.Empty);
            var cc = context.Get(this.Cc, string.Empty);
            var bcc = context.Get(this.Bcc, string.Empty);
            var subject = context.Get(this.Subject, string.Empty);
            var body = context.Get(this.Body, string.Empty);
            var attachment = context.Get(this.Attachment, string.Empty);
            var isHtmlBody = context.Get(this.IsHtmlBody, false);
            var isdraft = context.Get(this.IsDraft, false);

            dynamic app = Activator.CreateInstance(Type.GetTypeFromProgID("Outlook.Application"));

            var mailItem = app.CreateItem(0 /*  OlItemType.olMailItem */);

            if (from.ToString().Length > 0)
            {
                var fromAccount = app.Session.Accounts.Item(from.ToString());
                if (fromAccount == null)
                {
                    throw new Exception("Cannot find " + from + " account.");
                }
                mailItem.SendUsingAccount = fromAccount;
            }

            mailItem.To = to;
            mailItem.CC = cc;
            mailItem.BCC = bcc;
            mailItem.Subject = subject;
            if (isHtmlBody)
            {
                mailItem.HTMLBody = body;
            }
            else
            {
                mailItem.Body = body;
            }
            if (attachment.ToString().Length > 0)
            {
                mailItem.Attachments.Add(attachment.ToString(), 1 /* OlAttachmentType.olByValue */);
            }
            if (isdraft)
            {
                mailItem.Display();
            }
            else
            {
                mailItem.Send();
            }
        }
    }
}
