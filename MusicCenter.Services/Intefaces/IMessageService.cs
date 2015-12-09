using MusicCenter.Common.ViewModels.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCenter.Services.Intefaces
{
    public interface IMessageService
    {
        List<MessageLisItemViewModel> GetUserReceivedMessages(string email);

        List<MessageLisItemViewModel> GetUserSentMessages(string email);

        void SendUserMessage(NewMessageViewModel message);

        bool MessageRecipientsValid(string recipients);

        MessageDetailsViewModel GetMessageDetailsViewModel(int MessageId);

        NewMessageViewModel GetNewMessageViewModel(int? MessageId);

        void SetMessageReaded(int MessageId);
    }
}
