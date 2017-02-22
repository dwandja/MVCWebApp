using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INT422Project.Models;
using Microsoft.AspNet.Identity;

namespace INT422Project.ViewModels
{

    public class Repo_Messages : Repository
    {

        public IEnumerable<MessageFull> getMessageFull()
        {

            var objs = context.Messages.OrderBy(n => n.messageId);

            List<MessageFull> list = new List<MessageFull>();

            foreach (var item in objs)
            {

                MessageFull message = new MessageFull();
                message.messageId = item.messageId;
                message.standardMessage = item.standardMessage;
                message.customMessage = item.customMessage;
                //message.faculty = item.faculty;

                list.Add(message);

            }

            return list;

        }

        public MessageFull getMessageFull(int? id)
        {

            var obj = context.Messages.FirstOrDefault(n => n.messageId == id);

            MessageFull message = new MessageFull();
            message.messageId = obj.messageId;
            message.standardMessage = obj.standardMessage;
            message.customMessage = obj.customMessage;
            //message.faculty = obj.faculty;

            return message;

        }

        public IEnumerable<MessageList> getMessageList()
        {

            var all = context.Messages.OrderBy(n => n.messageId);

            List<MessageList> list = new List<MessageList>();

            foreach (var item in all)
            {

                MessageList message = new MessageList();
                message.messageId = item.messageId;
                message.standardMessage = item.standardMessage;
                message.customMessage = item.customMessage;
                //message.faculty = item.faculty;

                list.Add(message);

            }

            return list;

        }

        public MessageFull createMessage(MessageFull _message)
        {

            Message message = new Message(_message.standardMessage, _message.customMessage, null);

            string currentUserId = HttpContext.Current.User.Identity.GetUserId();
            ApplicationUser currentUser;
            currentUser = context.Users.FirstOrDefault(x => x.Id == currentUserId);

            context.Messages.Add(message);
            context.SaveChanges();

            return _message;

        }

    }

}