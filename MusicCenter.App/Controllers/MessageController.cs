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

        [Authorize]
        public ActionResult UserMessages()
        {
            List<MessageLisItemViewModel> userMessages = msgService.GetUserReceivedMessages(User.Identity.Name);

            return View(userMessages);
        }

        [Authorize]
        public ActionResult UserSentMessages()
        {
            List<MessageLisItemViewModel> userMessages = msgService.GetUserSentMessages(User.Identity.Name);

            return View(userMessages);
        }

        [Authorize]
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
        [Authorize]
        public ActionResult UserNewMessage(NewMessageViewModel model)
        {
            model.AuthorEmail = User.Identity.Name;

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
            if (!User.Identity.Name.ToLower().Equals(model.Author.ToLower()))
            {
                msgService.SetMessageReaded(model.Id);
            }
            return View(model);
        }
	}
}