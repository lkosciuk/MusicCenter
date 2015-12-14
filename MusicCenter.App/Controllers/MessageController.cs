using MusicCenter.App.Filters;
using MusicCenter.Common.ViewModels.Message;
using MusicCenter.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicCenter.App.Controllers
{
    public class MessageController : Controller
    {
        IMessageService msgService;

        public MessageController(IMessageService serv)
        {
            msgService = serv;
        }

        [UserAuthorize]
        public ActionResult UserMessages()
        {
            List<MessageLisItemViewModel> userMessages = msgService.GetUserReceivedMessages(Session["user"].ToString());

            return View(userMessages);
        }

        [UserAuthorize]
        public ActionResult UserSentMessages()
        {
            List<MessageLisItemViewModel> userMessages = msgService.GetUserSentMessages(Session["user"].ToString());

            return View(userMessages);
        }

        [UserAuthorize]
        public ActionResult UserNewMessage(int? MessageId)
        {
            NewMessageViewModel model = new NewMessageViewModel();

            if (MessageId.HasValue)
            {
                model = msgService.GetNewMessageViewModel(MessageId);
            }

            return View(model);
        }

        [HttpPost]
        [UserAuthorize]
        public ActionResult UserNewMessage(NewMessageViewModel model)
        {
            model.AuthorEmail = Session["user"].ToString();

            if (ModelState.IsValid)
            {
                if (msgService.MessageRecipientsValid(model.Recipients))
                {
                    msgService.SendMessage(model);

                    return RedirectToAction("UserSentMessages");
                    
                }
                else
                {
                    model.RecipientsErrorMsg = "Some of recipients doeas not exists";
                }

            }
            return View(model);
        }

        [UserAuthorize]
        public ActionResult UserMessageDetails(int MessageId)
        {
            MessageDetailsViewModel model = msgService.GetMessageDetailsViewModel(MessageId);
            if (!Session["user"].ToString().ToLower().Equals(model.Author.ToLower()))
            {
                msgService.SetMessageReaded(model.Id);
            }
            return View(model);
        }

        [BandAuthorize]
        public ActionResult BandMessages()
        {
            List<MessageLisItemViewModel> bandMsgs = msgService.GetBandReceivedMessages(Session["band"].ToString());

            return View(bandMsgs);
        }

        [BandAuthorize]
        public ActionResult BandSentMessages()
        {
            List<MessageLisItemViewModel> bandMessages = msgService.GetBandSentMessages(Session["band"].ToString());

            return View(bandMessages);
        }

        [BandAuthorize]
        public ActionResult BandNewMessage(int? MessageId)
        {
            NewMessageViewModel model = new NewMessageViewModel();

            if (MessageId.HasValue)
            {
                model = msgService.GetNewMessageViewModel(MessageId);
            }

            return View(model);
        }

        [HttpPost]
        [BandAuthorize]
        public ActionResult BandNewMessage(NewMessageViewModel model)
        {
            model.AuthorEmail = Session["band"].ToString();

            if (ModelState.IsValid)
            {
                if (msgService.MessageRecipientsValid(model.Recipients))
                {
                    msgService.SendMessage(model);

                    return RedirectToAction("BandSentMessages");
                }
                else
                {
                    model.RecipientsErrorMsg = "Some of recipients doeas not exists";
                }

            }
            return View(model);
        }

        [BandAuthorize]
        public ActionResult BandMessageDetails(int MessageId)
        {
            MessageDetailsViewModel model = msgService.GetMessageDetailsViewModel(MessageId);
            if (!Session["band"].ToString().ToLower().Equals(model.Author.ToLower()))
            {
                msgService.SetMessageReaded(model.Id);
            }
            return View(model);
        }

        [SessionAuthorize]
        public ActionResult SendBandMessage(string Recipient)
        {
            NewMessageViewModel model = new NewMessageViewModel();
            model.Recipients = Recipient;

            if (Session["user"] != null)
            {
                model.AuthorEmail = Session["user"].ToString();
            }
            else
            {
                model.AuthorEmail = Session["band"].ToString();
            }

            return View(model);
        }

        [HttpPost]
        [SessionAuthorize]
        public ActionResult SendBandMessage(NewMessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (msgService.MessageRecipientsValid(model.Recipients))
                {
                    msgService.SendMessage(model);
                    return View(model);
                }
                else
                {
                    model.RecipientsErrorMsg = "Some of recipients doeas not exists";
                }

            }

            return View(model);
        }
	}
}