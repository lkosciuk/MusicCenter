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
                    msgService.SendUserMessage(model);

                    return RedirectToAction("UserSentMessages");
                }
                else
                {
                    model.RecipientsErrorMsg = "Some of recipients doeas not exists";
                }

            }
            return View(model);
        }

        public ActionResult MessageDetails(int MessageId)
        {
            MessageDetailsViewModel model = msgService.GetMessageDetailsViewModel(MessageId);
            if (!Session["user"].ToString().ToLower().Equals(model.Author.ToLower()))
            {
                msgService.SetMessageReaded(model.Id);
            }
            return View(model);
        }
	}
}