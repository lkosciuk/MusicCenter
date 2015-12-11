using MusicCenter.Common.ViewModels.Message;
using MusicCenter.Dal.EntityModels;
using MusicCenter.Services.Intefaces;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MusicCenter.Services.Services
{
    public class MessageService : BaseService<Message>, IMessageService
    {
        public MessageService(IUnitOfWork u)
            : base(u)
        {
            
        }

        public List<MessageLisItemViewModel> GetUserReceivedMessages(string email)
        {

            var userMessages = _repo
                                .Queryable().Where(m => m.UserReceivers.Any(u => u.email == email))
                                .Include(m => m.UserAuthor).Include(m => m.BandAuthor).Include(m => m.BandReceivers).Include(m => m.UserReceivers)
                                .ToList()
                                .Select(msg => new MessageLisItemViewModel()
                                {
                                    Id = msg.Id,
                                    Author = msg.UserAuthor != null ? msg.UserAuthor.email : msg.BandAuthor.name,
                                    Recipients = string.Join(string.Join(String.Empty, msg.UserReceivers.Select(u => u.email)), msg.BandReceivers.Select(b => b.name)),
                                    Title = msg.title,
                                    Content = msg.content,
                                    IsReaded = msg.isReaded,
                                    SentDate = msg.sentDate
                                }).OrderBy(d => d.SentDate);

            return userMessages.ToList();
        }


        public List<MessageLisItemViewModel> GetUserSentMessages(string email)
        {
            var userMsgs = _repo
                                .Queryable().Where(m => m.UserAuthor.email == email)
                                .Include(m => m.UserAuthor).Include(m => m.BandAuthor).Include(m => m.BandReceivers).Include(m => m.UserReceivers)
                                .ToList()
                                .Select(msg => new MessageLisItemViewModel()
                                {
                                    Id = msg.Id,
                                    Author = msg.UserAuthor != null ? msg.UserAuthor.email : msg.BandAuthor.name,
                                    Recipients = string.Join(string.Join(String.Empty, msg.UserReceivers.Select(u => u.email)), msg.BandReceivers.Select(b => b.name)),
                                    Title = msg.title,
                                    Content = msg.content,
                                    IsReaded = msg.isReaded,
                                    SentDate = msg.sentDate
                                }).OrderBy(d => d.SentDate);

            return userMsgs.ToList();
        }


        public void SendMessage(NewMessageViewModel message)
        {
            Users userAuthor = _unitOfWork.Repository<Users>().Queryable().FirstOrDefault(u => u.email == message.AuthorEmail);
            Band bandAuthor = _unitOfWork.Repository<Band>().Queryable().FirstOrDefault(u => u.name == message.AuthorEmail);

            Message newMsg = new Message()
            {
                UserAuthor = userAuthor,
                BandAuthor = bandAuthor,
                title = message.Title,
                content = message.Content,
                sentDate = DateTime.Now,
                isReaded = false,
                ObjectState = ObjectState.Added
            };

            Regex.Replace(message.Recipients, @"\s+", "");

            string[] Recipients = message.Recipients.Split(',');

            foreach (var recipient in Recipients)
            {
                Users recipientUser = _unitOfWork.Repository<Users>().Queryable().FirstOrDefault(u => u.email.ToLower() == recipient.ToLower());
                Band recipientBand = _unitOfWork.Repository<Band>().Queryable().FirstOrDefault(u => u.name.ToLower() == recipient.ToLower());

                if (recipientUser != null)
                {
                    newMsg.UserReceivers.Add(recipientUser);
                }
                else if (recipientBand != null)
                {
                    newMsg.BandReceivers.Add(recipientBand);
                }

                _repo.InsertOrUpdateGraph(newMsg);
                _unitOfWork.SaveChanges();

                recipientUser = null;
                recipientBand = null;
            }

        }


        public bool MessageRecipientsValid(string recipients)
        {
            Regex.Replace(recipients, @"\s+", "");

            string[] recipientsList = recipients.Split(',');

            foreach (var item in recipientsList)
            {
                if (!_unitOfWork.Repository<Users>().Queryable().Any(u => u.email.ToLower() == item.ToLower()) &&
                    !_unitOfWork.Repository<Band>().Queryable().Any(b => b.name.ToLower() == item.ToLower()))
                {
                    return false;
                }
            }

            return true;
        }

        public MessageDetailsViewModel GetMessageDetailsViewModel(int MessageId)
        {
            return _unitOfWork.Repository<Message>().Queryable()
                    .Where(m => m.Id == MessageId)
                    .Select(msg => new MessageDetailsViewModel()
                    {
                        Id = msg.Id,
                        Author = msg.UserAuthor != null ? msg.UserAuthor.email : msg.BandAuthor.name,
                        Title = msg.title,
                        Content = msg.content,
                        SentDate = msg.sentDate

                    }).FirstOrDefault();
        }


        public NewMessageViewModel GetNewMessageViewModel(int? MessageId)
        {
            return _repo
                   .Queryable().Where(m => m.Id == MessageId)
                   .Select(msg => new NewMessageViewModel()
                   {
                       Recipients = msg.UserAuthor != null ? msg.UserAuthor.email : msg.BandAuthor.name,
                       Title = msg.title

                   }).FirstOrDefault(); ;
        }

        public void SetMessageReaded(int MessageId)
        {
            Message currentMsg = _repo.Queryable().FirstOrDefault(m => m.Id == MessageId);
            currentMsg.isReaded = true;
            currentMsg.ObjectState = ObjectState.Modified;

            _unitOfWork.SaveChanges();
        }


        public List<MessageLisItemViewModel> GetBandReceivedMessages(string BandName)
        {
            var bandMessages = _repo
                                .Queryable().Where(m => m.BandReceivers.Any(u => u.name == BandName))
                                .Include(m => m.UserAuthor).Include(m => m.BandAuthor).Include(m => m.BandReceivers).Include(m => m.UserReceivers)
                                .ToList()
                                .Select(msg => new MessageLisItemViewModel()
                                {
                                    Id = msg.Id,
                                    Author = msg.UserAuthor != null ? msg.UserAuthor.email : msg.BandAuthor.name,
                                    Recipients = string.Join(string.Join(String.Empty, msg.UserReceivers.Select(u => u.email)), msg.BandReceivers.Select(b => b.name)),
                                    Title = msg.title,
                                    Content = msg.content,
                                    IsReaded = msg.isReaded,
                                    SentDate = msg.sentDate
                                }).OrderBy(d => d.SentDate);

            return bandMessages.ToList();
        }


        public List<MessageLisItemViewModel> GetBandSentMessages(string BandName)
        {
            var userMsgs = _repo
                                .Queryable().Where(m => m.BandAuthor.name == BandName)
                                .Include(m => m.UserAuthor).Include(m => m.BandAuthor).Include(m => m.BandReceivers).Include(m => m.UserReceivers)
                                .ToList()
                                .Select(msg => new MessageLisItemViewModel()
                                {
                                    Id = msg.Id,
                                    Author = msg.UserAuthor != null ? msg.UserAuthor.email : msg.BandAuthor.name,
                                    Recipients = string.Join(string.Join(String.Empty, msg.UserReceivers.Select(u => u.email)), msg.BandReceivers.Select(b => b.name)),
                                    Title = msg.title,
                                    Content = msg.content,
                                    IsReaded = msg.isReaded,
                                    SentDate = msg.sentDate
                                }).OrderBy(d => d.SentDate);

            return userMsgs.ToList();
        }
    }
}
