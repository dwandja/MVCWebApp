using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcWebApp.Models;
using Microsoft.AspNet.Identity;
using AutoMapper;

namespace MvcWebApp.ViewModels
{

    public class Repo_Message : Repository
    {

        public IEnumerable<MessageFull> getMessageFull()
        {

            var objs = context.Messages.Include("Faculty").OrderBy(n => n.MessageId);

            List<MessageFull> list = new List<MessageFull>();

            foreach (var item in objs)
            {

                MessageFull message = new MessageFull();
                message.MessageId = item.MessageId;
                message.StandardMessage = item.StandardMessage;
                message.CustomMessage = item.CustomMessage;
                message.Faculty = item.Faculty;

                list.Add(message);

            }

            return list;

        }

        public MessageFull getMessageFull(int? id)
        {

            var obj = context.Messages.Include("Faculty").FirstOrDefault(n => n.MessageId == id);

            MessageFull message = new MessageFull();
            message.MessageId = obj.MessageId;
            message.StandardMessage = obj.StandardMessage;
            message.CustomMessage = obj.CustomMessage;
            message.Faculty = obj.Faculty;

            return message;

        }

        public IEnumerable<MessageList> getMessageList()
        {

            var all = context.Messages.Include("Faculty").OrderBy(n => n.MessageId);

            List<MessageList> list = new List<MessageList>();

            foreach (var item in all)
            {

                MessageList message = new MessageList();
                message.MessageId = item.MessageId;
                message.StandardMessage = item.StandardMessage;
                message.CustomMessage = item.CustomMessage;
                message.Faculty = item.Faculty;

                list.Add(message);

            }

            return list;

        }

    }

}